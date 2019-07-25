using HelloService.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HelloService.Entities.Request;
using HelloService.DataLogic.Implement;
using HelloService.DataAccess.Implement;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloService.Controllers
{
    public class UserController : Controller
    {
        private readonly UserLogic userLogic;
        public UserController()
        {
            userLogic = new UserLogic();
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var headers = Request.Headers;
            var device = UserHelper.GetDeviceFromHeader(headers);
            if (device == null) return BadRequest();
            var user = userLogic.FindByPhoneNumber(request.Phone);
            if (user == null) return NoContent();
            if (!user.Active) return NoContent();
            if (user.SecurityCode != request.SecurityCode) return NoContent();
            var result = userLogic.Login(user, device);
            return Ok(result);
        }

        [HttpPost, Authorize]
        public IActionResult Logout()
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            userLogic.Logout(user);
            return Ok();
        }

        [HttpPost]
        public IActionResult Registration([FromBody] RegisterRequest request)
        {
            var result = userLogic.Register(request);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult VerificationCode(string phone, string code)
        {
            var request = new ValidationCodeRequest { Phone = phone, Code = code };
            var isValid = userLogic.IsValidVerificationCode(request);
            if (isValid) return Ok();
            else return Forbid();
        }

        [HttpPost]
        public IActionResult ResendCode([FromBody] ResendCodeRequest request)
        {
            var success = userLogic.ResendCode(request.Phone);
            if (success) return Ok();
            else return Forbid();
        }

        [HttpPost, Authorize]
        public IActionResult UpdateProfilePicture([FromBody] UpdateProfilePictureRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var success = userLogic.UpdateProfilePicture(user, request.Content);
            if (success) return Ok();
            else return Forbid();
        }

        [HttpPost, Authorize]
        public IActionResult UpdateName([FromBody] UpdateNameRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var success = userLogic.UpdateName(user, request.Name);
            if (success) return Ok();
            else return Forbid();
        }

        [HttpPost, Authorize]
        public IActionResult UpdateAbout([FromBody] UpdateAboutRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var success = userLogic.UpdateAbout(user, request.About);
            if (success) return Ok();
            else return Forbid();
        }


    }
}
