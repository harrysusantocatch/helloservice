using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HelloService.DataAccess.Implement;
using HelloService.DataLogic.Interface;
using HelloService.Entities.DB;
using HelloService.Entities.Model;
using HelloService.Entities.Request;
using HelloService.Entities.Response;
using HelloService.Helper;
using Microsoft.IdentityModel.Tokens;

namespace HelloService.DataLogic.Implement
{
    public class UserLogic : IUserLogic
    {
        private static UserLogic _instance;
        public static UserLogic Instance => _instance ?? (_instance = new UserLogic());
        private readonly UserDao userDao;
        private readonly RegistrationCodeDao registerDao;
        public UserLogic()
        {
            userDao = new UserDao();
            registerDao = new RegistrationCodeDao();
        }

        public UserResponse Register(RegisterRequest request)
        {
            if (request.Phone == null || request.Device == null) return null;
            if (request.Device.Token == null) return null;
            var findUser = userDao.FindByPhoneNumber(request.Phone);
            if (findUser == null)
            {
                var model = new User
                {
                    Phone = Encryptor.EncryptSHA256(request.Phone),
                    Device = request.Device,
                    Active = false
                };
                var user = userDao.InsertAndGet(model);
                if (user != null)
                {
                    SaveAndSendRegistrationCode(user);
                    return new UserResponse(user);
                }
                else return null;
            }
            else
            {
                if (findUser.Active) return null;
                findUser.Device = request.Device;
                findUser.Active = false;
                userDao.Update(findUser, new string[] { "Device", "Active" });
                SaveAndSendRegistrationCode(findUser);
                return new UserResponse(findUser);
            }
        }

        public bool VerificationCode(ValidationCodeRequest request)
        {
            var registrationCode = registerDao.FindByPhoneNumber(request.Phone);
            if (registrationCode == null) return false;
            if (DateTime.Compare(registrationCode.ExpireDate, Constant.SERVER_TIME) < 0) return false;
            if (registrationCode.Code != request.Code) return false;
            var user = userDao.FindByPhoneNumber(request.Phone);
            if (user == null) return false;
            user.Active = true;
            userDao.Update(user, new string[] { "Active" });
            return true;
        }

        public bool ResendCode(string phoneNumber)
        {
            var user = userDao.FindByPhoneNumber(phoneNumber);
            if (user == null) return false;
            if (user.Active) return false;
            var code = GenerateCode();
            var registrationCode = registerDao.FindByPhoneNumber(phoneNumber);
            if (registrationCode != null) registerDao.Delete(registrationCode);
            var success = registerDao.Insert(new RegistrationCode { Code = code, ExpireDate = Constant.SERVER_TIME.AddMinutes(10), Phone = Encryptor.EncryptSHA256(phoneNumber) });
            if (success)
            {
                // TODO send notification with code
                return true;
            }
            return false;
        }

        public User FindByPhoneNumber(string phoneNumber)
        {
            var user = userDao.FindByPhoneNumber(phoneNumber);
            return user;
        }

        public LoginResponse Login(User user, Device device)
        {
            Claim[] claims =
                {
                    new Claim(ClaimTypes.Name, "Default"),
                    new Claim(ClaimTypes.SerialNumber, device.Token),
                    new Claim(ClaimTypes.Sid, user.ID.ToString())
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constant.KEY_ENCRYPT));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(Startup.Configuration["Jwt:Issuer"],
                                             Startup.Configuration["Jwt:Issuer"],
                                             claims,
                                             expires: Constant.SERVER_TIME.AddDays(7),
                                             signingCredentials: creds);
            user.Device = device;
            userDao.Update(user, new string[] { "Device" });
            var strToken = new JwtSecurityTokenHandler().WriteToken(token);
            var strExpireDate = token.ValidTo.ToString("MM/dd/yy H:mm:ss");
            return new LoginResponse { Token = strToken, Expire = strExpireDate };
        }

        public void Logout(User user)
        {
            user.Device = null;
            userDao.Update(user, new string[] { "Device" });
        }

        private void SaveAndSendRegistrationCode(User user)
        {
            var code = GenerateCode();
            var registrationCode = registerDao.FindByPhoneNumberEncrypted(user.Phone);
            if (registrationCode != null) registerDao.Delete(registrationCode);
            var success = registerDao.Insert(new RegistrationCode { Code = code, ExpireDate = Constant.SERVER_TIME.AddMinutes(10), Phone = user.Phone});
            if (success)
            {
                // TODO send notification with code
            }
        }

        private bool SaveAndSendRegistrationCode(string phone)
        {
            var code = GenerateCode();
            var registrationCode = registerDao.FindByPhoneNumber(phone);
            if (registrationCode != null) registerDao.Delete(registrationCode);
            var success = registerDao.Insert(new RegistrationCode { Code = code, ExpireDate = Constant.SERVER_TIME.AddMinutes(10), Phone = Encryptor.EncryptSHA256(phone) });
            if (success)
            {
                // TODO send notification with code
                return true;
            }
            return false;
        }

        private string GenerateCode()
        {
            int defaultLength = 4;
            int code = new Random().Next(0, 9999);
            string strCode = code.ToString();
            int lengthToken = strCode.Length;
            int marginLength = defaultLength - lengthToken;
            if (marginLength > 0)
            {
                string zero = "";
                for (int i = 1; i <= marginLength; i++)
                {
                    zero += "0";
                }
                strCode = zero + strCode;
            }
            return strCode;
        }

        public bool UpdateProfilePicture(User user, Blob content)
        {
            user.ProfilePicture = content;
            return userDao.Update(user, new string[] { "ProfilePicture" });
        }

        public bool UpdateName(User user, string name)
        {
            user.FullName = name;
            return userDao.Update(user, new string[] { "FullName" });
        }

        public bool UpdateAbout(User user, string about)
        {
            user.About = about;
            return userDao.Update(user, new string[] { "About" });
        }
    }
}
