using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelloService.Controllers
{
    public class StoryController : BaseController
    {
        public IActionResult GetListStory()
        {
            return Ok();
        }
        public IActionResult GetMyStory()
        {
            return Ok();
        }

        public IActionResult AddStroy()
        {
            return Ok();
        }

        public IActionResult RemoveStory()
        {
            return Ok();
        }
    }
}