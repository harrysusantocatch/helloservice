using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloService.DataLogic.Implement;
using HelloService.Entities.Request;
using HelloService.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloService.Controllers
{
    public class ChatController : BaseController
    {
        [HttpGet, Authorize]
        public IActionResult GetChatRoom(string gmt)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var responses = ChatLogic.Instance.GetChatRooms(user, gmt);
            return Ok(responses);
        }

        [HttpPost, Authorize]
        public IActionResult CreateChatRoom(ChatRoomRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = ChatLogic.Instance.CreatChatRoom(request);
            if (response) return Ok();
            else return Forbid();
        }

        [HttpPost, Authorize]
        public IActionResult SendMessage(SendMessageRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = ChatLogic.Instance.SendMessage(user, request);
            if (response) return Ok();
            else return Forbid();
        }

        [HttpGet, Authorize]
        public IActionResult GetMessages(string chatRoomID, string lastDate)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = ChatLogic.Instance.GetMessage(user, chatRoomID, lastDate);
            return Ok(response);
        }
        
        [HttpGet, Authorize]
        public IActionResult GetLastSeen(string phone, string gmt)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = ChatLogic.Instance.GetLastSeenByPhone(phone, gmt);
            if (response == null) return NoContent();
            else return Ok(response);
        }

        [HttpPut, Authorize]
        public IActionResult InvokeOnline()
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = ChatLogic.Instance.InvokeStatus(user, "Online");
            if (!response) return Forbid();
            else return Ok();
        }

        [HttpPut, Authorize]
        public IActionResult InvokeOffline()
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = ChatLogic.Instance.InvokeStatus(user, Constant.SERVER_TIME.Ticks.ToString());
            if (!response) return Forbid();
            else return Ok();
        }

        [HttpDelete, Authorize]
        public IActionResult RemoveMessageForMe(string messageID)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = ChatLogic.Instance.RemoveMessageForMe(user, messageID);
            if (response) return Ok();
            else return Forbid();
        }

        [HttpDelete, Authorize]
        public IActionResult RemoveMessageForAll(string messageID)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = ChatLogic.Instance.RemoveMessageForAll(user, messageID);
            if (response) return Ok();
            else return Forbid();
        }

        [HttpDelete, Authorize]
        public IActionResult RemoveChatRoom(string chatRoomID)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = ChatLogic.Instance.RemoveChatRoom(user, chatRoomID);
            if (response) return Ok();
            else return Forbid();
        }
    }
}
