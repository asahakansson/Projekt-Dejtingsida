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
        //Sparar meddelanden i databasen
        [HttpPost]
        [Route("add")]
        public void PushEntry(MessageModel model)
        {
            model.SendDate = DateTimeOffset.Now;
            var ctx = new ProfileDbContext();
            ctx.Messages.Add(model);
            ctx.SaveChanges();
        }
        //Hämtar en lista på meddelanden ur databasen
        [HttpGet]
        [Route("list")]
        public List<MessageViewModel> GetMessages(string userId)
        {
            var ctx = new ProfileDbContext();
            var messages = ctx.Messages.Where(m => m.Reciever == userId);
            //Tabellerna Messages och Profiles joinas för att namnet på avsändaren ska kunna visas
            var list = messages.Join(ctx.Profiles, m => m.Sender, p => p.UserID, (m, p) => new { m, p })
                //Produkten av joinen är den nya tabellen a
                .Select(a =>
                    new MessageViewModel
                    {
                        MessageId = a.m.MessageId,
                        MessageText = a.m.MessageText,
                        Reciever = a.m.Reciever,
                        SenderName = a.p.FirstName + " " + a.p.LastName,
                        SendDate = a.m.SendDate
                    }
                ).ToList();

            return list;
        }
    }
}
