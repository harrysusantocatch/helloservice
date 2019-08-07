using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HelloService.Entities.Request;
using HelloService.DataLogic.Implement;
using HelloService.Entities.Response;

namespace HelloService.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost]
        public IActionResult Registration([FromBody] RegisterRequest request)
        {
            var result = UserLogic.Instance.Register(request);
            if (result is MessageErrorResponse) return this.NotAcceptable((MessageErrorResponse)result);
            else return Ok(result);
        }

        [HttpPut]
        public IActionResult VerificationCode([FromBody]ValidationCodeRequest request)
        {
            var result = UserLogic.Instance.VerificationCode(request);
            if (result is MessageErrorResponse) return this.NotAcceptable((MessageErrorResponse)result);
            else return Ok(result);
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
            var device = UserLogic.Instance.GetDeviceFromHeader(headers);
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

        [HttpPut, Authorize]
        public IActionResult UpdateProfilePicture([FromBody] UpdateProfilePictureRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var success = UserLogic.Instance.UpdateProfilePicture(user, request.Content);
            if (success) return Ok();
            else return Forbid();
        }

        [HttpPut, Authorize]
        public IActionResult UpdateName([FromBody] UpdateNameRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var success = UserLogic.Instance.UpdateName(user, request.Name);
            if (success) return Ok();
            else return Forbid();
        }

        [HttpPut, Authorize]
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
