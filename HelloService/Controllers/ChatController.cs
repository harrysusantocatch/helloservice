﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloService.DataLogic.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloService.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatRoomLogic chatRoomLogic;

        public ChatController()
        {
            chatRoomLogic = new ChatRoomLogic();
        }

        [HttpGet, Authorize]
        public IActionResult GetChatRoom(string gmt)
        {
            var user = this.GetUserAuthorize();
            if (user == null) return Unauthorized();
            var responses = chatRoomLogic.GetChatRooms(user, gmt);
            return Ok(responses);
        }

        public IActionResult CreateChatRoom()
        {
            return Ok();
        }

        public IActionResult RemoveChatRoom()
        {
            return Ok();
        }

        public IActionResult GetMessages()
        {
            return Ok();
        }

        public IActionResult SendMessage()
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
