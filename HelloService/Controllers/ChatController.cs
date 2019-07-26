using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloService.DataLogic.Implement;
using HelloService.Entities.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloService.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatLogic chatLogic;

        public ChatController()
        {
            chatLogic = new ChatLogic();
        }

        [HttpGet, Authorize]
        public IActionResult GetChatRoom(string gmt)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var responses = chatLogic.GetChatRooms(user, gmt);
            return Ok(responses);
        }

        [HttpPost, Authorize]
        public IActionResult CreateChatRoom(ChatRoomRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = chatLogic.CreatChatRoom(request);
            if (response) return Ok();
            else return Forbid();
        }

        [HttpPost, Authorize]
        public IActionResult SendMessage(SendMessageRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = chatLogic.SendMessage(user, request);
            if (response) return Ok();
            else return Forbid();
        }

        [HttpGet, Authorize]
        public IActionResult GetMessages(string chatRoomID, string lastDate)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = chatLogic.GetMessage(user, chatRoomID, lastDate);
            return Ok();
        }

        public IActionResult RemoveChatRoom()
        {
            return Ok();
        }

        public IActionResult RemoveMessage()
        {
            return Ok();
        }

        public IActionResult GetLastSeen()
        {
            return Ok();
        }

        public IActionResult InvokeOnline()
        {
            return Ok();
        }

        public IActionResult InvokeOffline()
        {
            return Ok();
        }


    }
}
