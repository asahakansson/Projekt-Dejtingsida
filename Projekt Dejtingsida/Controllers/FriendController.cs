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
        public ActionResult RequestList(string friendID)
        {
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            // A few diffrent validation
            // First we check that the friendID isnt null and it exist.
            var friendExist = ctx.Profiles.Any(p => p.UserID == friendID);
            if ((!(friendID == null)) && friendExist) {
                // Then check if an request is already made
                if (!(ctx.FriendRequestModels.Any(f => 
                (f.Person1 == currentID && f.Person2 == friendID) ||
                (f.Person1 == friendID && f.Person2 == currentID)
                ))){
                    // Then we check if the users are already friends
                    if(!(ctx.Friends.Any(f =>
                    (f.Person1 == currentID && f.Person2 == friendID) ||
                    (f.Person1 == friendID && f.Person2 == currentID)))){
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
            else
            {
                return View(new RequestSent { Success = false, Error = "Something went wrong, please try again" });
            }
        }
        public ActionResult OutgoingRequest()
        {
            var ctx = new ProfileDbContext();
            var currentUser = User.Identity.GetUserId();
            // Get a list of all friend requests made by current user.
            var listOfRequests = ctx.FriendRequestModels.Where(f => f.Person1 == currentUser);
            // Get a list of all profiles
            var listOfProfiles = ctx.Profiles.ToList();
            //var requestList = new RequestList().RequestLists;
            //if (listOfRequests.Any()) {
            //    foreach (var request in listOfRequests)
            //    {
            //        var user = listOfProfiles.FirstOrDefault(u => u.UserID == request.Person2);
            //        var addRequest = new FriendRequestList
            //        {
            //            Firstname = user.FirstName,
            //            Lastname = user.LastName
            //        };
            //        requestList.Add(addRequest);
            //    }
            //    return View(requestList);
            //}
            return View(listOfRequests);
        }
        public ActionResult IncommingRequest()
        {
            var ctx = new ProfileDbContext();
            var currentUser = User.Identity.GetUserId();
            // Get a list of all friend requests made by current user.
            var listOfRequests = ctx.FriendRequestModels.Where(f => f.Person2 == currentUser);
            // Get a list of all profiles
            return View(listOfRequests);
        }

        //public ActionResult FriendList()
        //{
        //    var ctx = new ProfileDbContext();
        //    var currentID = User.Identity.GetUserId();
        //    return View(FriendList)
        // }
    }
}