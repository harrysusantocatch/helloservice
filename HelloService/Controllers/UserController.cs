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
        [HttpPost]
        public IActionResult Registration([FromBody] RegisterRequest request)
        {
            var result = UserLogic.Instance.Register(request);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult VerificationCode([FromBody]ValidationCodeRequest request)
        {
            var isValid = UserLogic.Instance.VerificationCode(request);
            if (isValid) return Ok();
            else return NoContent();
        }

        [HttpPost]
        public IActionResult ResendCode([FromBody] ResendCodeRequest request)
        {
            var success = UserLogic.Instance.ResendCode(request.Phone);
            if (success) return Ok();
            else return NoContent();
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var headers = Request.Headers;
            var device = UserHelper.GetDeviceFromHeader(headers);
            if (device == null) return BadRequest();
            var user = UserLogic.Instance.FindByPhoneNumber(request.Phone);
            if (user == null) return NoContent();
            if (!user.Active) return NoContent();
            //if (user.SecurityCode != request.SecurityCode) return NoContent();
            var result = UserLogic.Instance.Login(user, device);
            return Ok(result);
        }

        [HttpPost, Authorize]
        public IActionResult Logout()
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            UserLogic.Instance.Logout(user);
            return Ok();
        }

        [HttpPost, Authorize]
        public IActionResult UpdateProfilePicture([FromBody] UpdateProfilePictureRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var success = UserLogic.Instance.UpdateProfilePicture(user, request.Content);
            if (success) return Ok();
            else return Forbid();
        }

        [HttpPost, Authorize]
        public IActionResult UpdateName([FromBody] UpdateNameRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var success = UserLogic.Instance.UpdateName(user, request.Name);
            if (success) return Ok();
            else return Forbid();
        }

        [HttpPost, Authorize]
        public IActionResult UpdateAbout([FromBody] UpdateAboutRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var success = UserLogic.Instance.UpdateAbout(user, request.About);
            if (success) return Ok();
            else return Forbid();
        }


    }
}
