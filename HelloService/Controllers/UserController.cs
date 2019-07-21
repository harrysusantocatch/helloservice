using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloService.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult Registration()
        {
            return Ok();
        }

        public IActionResult Login()
        {
            return Ok();
        }

        public IActionResult Logout()
        {
            return Ok();
        }

        public IActionResult VerificationCode()
        {
            return Ok();
        }

        public IActionResult ResendCode()
        {
            return Ok();
        }

        public IActionResult UpdateProfilePicture()
        {
            return Ok();
        }

        public IActionResult UpdateName()
        {
            return Ok();
        }

        public IActionResult UpdateAbout()
        {
            return Ok();
        }


    }
}
