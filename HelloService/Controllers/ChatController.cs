using HelloService.DataLogic.Implement;
using HelloService.Entities.Request;
using HelloService.Entities.Response;
using HelloService.Extension;
using HelloService.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult CreateChatRoom([FromBody]ChatRoomRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = ChatLogic.Instance.CreatChatRoom(user, request);
            if (response is MessageErrorResponse) return this.NotAcceptable((MessageErrorResponse)response);
            else return Ok(response);
        }

        [HttpPost, Authorize]
        public IActionResult SendMessage([FromBody]SendMessageRequest request)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = ChatLogic.Instance.SendMessage(user, request);
            if (response) return Ok();
            else return Forbid();
        }

        [HttpGet, Authorize]
        public IActionResult GetMessages(string chatRoomID, string longLastDate)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var response = ChatLogic.Instance.GetMessage(user, chatRoomID, longLastDate);
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
            ChatLogic.Instance.InvokeStatus(user, "Online");
            return Ok();
        }

        [HttpPut, Authorize]
        public IActionResult InvokeOffline()
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            ChatLogic.Instance.InvokeStatus(user, Constant.SERVER_TIME.ToUnixLong().ToString());
            return Ok();
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
