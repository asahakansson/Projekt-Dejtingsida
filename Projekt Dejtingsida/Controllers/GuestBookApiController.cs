using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Projekt_Dejtingsida.Models;

namespace Projekt_Dejtingsida.Controllers
{
    [RoutePrefix("api/guestbook")]
    public class GuestBookApiController : ApiController
    {
        [HttpPost]
        [Route("add")]
        public void PushEntry(MessageModel model)
        {

        }

        [HttpGet]
        [Route("list")]
        public List<MessageViewModel> GetMessages(string userId)
        {
            return new List<MessageViewModel>()
            {
                new MessageViewModel
                {
                    MessageId = 9,
                    MessageText = "Mupp",
                    Reciever = "Du",
                    SenderName = "Klas",
                    SendDate = DateTimeOffset.Now
                },
                new MessageViewModel
                {
                    MessageId = 91,
                    MessageText = "Mupp!!",
                    Reciever = "Du",
                    SenderName = "Klas",
                    SendDate = DateTimeOffset.Now
                }
            };
        }
    }
}
