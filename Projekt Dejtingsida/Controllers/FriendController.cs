using Microsoft.AspNet.Identity;
using Projekt_Dejtingsida.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt_Dejtingsida.Controllers
{

    [Authorize]
    public class FriendController : Controller
    {
        // GET: Friend
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SendRequest(string friendID)
        {
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            // A few diffrent validation
            // First check if an request is already made
            if(!(ctx.FriendRequestModels.Any(f => 
            (f.Person1 == currentID && f.Person2 == friendID) ||
            (f.Person1 == friendID && f.Person2 == currentID)
            ))){
                // Then we check if the users are already friends
                if(!(ctx.Friends.Any(f =>
                (f.Person1 == currentID && f.Person2 == friendID) ||
                (f.Person1 == friendID && f.Person2 == currentID)
                ))){
                    ctx.FriendRequestModels.Add(new FriendRequestModels
                    {
                        Person1 = currentID,
                        Person2 = friendID
                    });
                    ctx.SaveChanges();
                    return View(new RequestSent { Success = true });
                }
                else
                {
                    return View(new RequestSent { Success = false, Error = "Already friends" });
                }
            }
            else
            {
                return View(new RequestSent { Success = false, Error = "Request is already sent" });
            }
        }
    }
}