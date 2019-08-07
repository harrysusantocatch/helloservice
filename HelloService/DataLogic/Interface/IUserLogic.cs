using HelloService.Entities.DB;
using HelloService.Entities.Model;
using HelloService.Entities.Request;
using HelloService.Entities.Response;

namespace HelloService.DataLogic.Interface
{
    public interface IUserLogic
    {
        User FindByPhoneNumber(string phoneNUmber);
        LoginResponse Login(User user, Device device);
        void Logout(User user);
        object Register(RegisterRequest request);
        object VerificationCode(ValidationCodeRequest request);
        bool ResendCode(string phoneNumber);
        bool UpdateProfilePicture(User user, Blob content);
        bool UpdateName(User user, string name);
        bool UpdateAbout(User user, string about);
    }
}
