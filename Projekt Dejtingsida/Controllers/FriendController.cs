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
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            var incommingRequests = ctx.FriendRequestModels.Where(f => f.Person2 == currentID);
            var outgoingrequests = ctx.FriendRequestModels.Where(f => f.Person1 == currentID);
            var pending = new PendingRequests { Incomming = incommingRequests.Count(), Outgoing = outgoingrequests.Count() };
            return View(pending);
        }
        [HttpGet]
        public ActionResult RequestList(string friendID)
        {
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            // A few diffrent validation
            // First we check that the friendID isnt null and it exist.
            var friendExist = ctx.Profiles.Any(p => p.UserID == friendID);
            if ((!(friendID == null)) && friendExist)
            {
                // Then check if an request is already made
                if (!(ctx.FriendRequestModels.Any(f =>
                (f.Person1 == currentID && f.Person2 == friendID) ||
                (f.Person1 == friendID && f.Person2 == currentID)
                )))
                {
                    // Then we check if the users are already friends
                    if (!(ctx.Friends.Any(f =>
                     (f.Person1 == currentID && f.Person2 == friendID) ||
                     (f.Person1 == friendID && f.Person2 == currentID))))
                    {
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
        public ActionResult IncommingList()
        {
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            var listOfRequests = ctx.FriendRequestModels.Where(f => f.Person2 == currentID);
            var listOfProfiles = ctx.Profiles.ToList();
            List<FriendRequestList> listToSend = new List<FriendRequestList>();

            foreach (var u in listOfRequests)
            {
                var AddId = u.Person1;
                var User = listOfProfiles.FirstOrDefault(p => p.UserID == AddId);

                var AddFname = User.FirstName;
                var AddLName = User.LastName;

                var AddThis = new FriendRequestList
                {
                    Id = u.Id,
                    UserId = AddId,
                    Firstname = AddFname,
                    Lastname = AddLName
                };
                listToSend.Add(AddThis);
            }
            if (listToSend.Any())
            {
                return View(listToSend);
            }
            else
            {
                return View();
            }
        }
        public ActionResult OutgoingList()
        {
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            var listOfRequests = ctx.FriendRequestModels.Where(f => f.Person1 == currentID);
            var listOfProfiles = ctx.Profiles.ToList();
            List<FriendRequestList> listToSend = new List<FriendRequestList>();

            foreach (var u in listOfRequests)
            {
                var AddId = u.Person2;
                var User = listOfProfiles.FirstOrDefault(p => p.UserID == AddId);

                var AddFname = User.FirstName;
                var AddLName = User.LastName;

                var AddThis = new FriendRequestList
                {
                    Id = u.Id,
                    UserId = AddId,
                    Firstname = AddFname,
                    Lastname = AddLName
                };
                listToSend.Add(AddThis);
            }
            if (listToSend.Any())
            {
                return View(listToSend);
            }
            else
            {
                return View();
            }
        }
        public ActionResult RemoveRequest(int removeID)
        {
            var ctx = new ProfileDbContext();
            var remove = ctx.FriendRequestModels.FirstOrDefault(f => f.Id == removeID);
            ctx.FriendRequestModels.Remove(remove);
            ctx.SaveChanges();

            return RedirectToAction("OutgoingList");
        }
        public ActionResult RequestRespone(int requestID, bool acceptRequest)
        {
            var ctx = new ProfileDbContext();
            var friendRequest = ctx.FriendRequestModels.FirstOrDefault(f => f.Id == requestID);
            if (acceptRequest)
            {
                var addFriend = new FriendModel { Person1 = friendRequest.Person1, Person2 = friendRequest.Person2 };
                ctx.Friends.Add(addFriend);
                ctx.SaveChanges();
            }

            var remove = ctx.FriendRequestModels.FirstOrDefault(f => f.Id == requestID);
            ctx.FriendRequestModels.Remove(remove);
            ctx.SaveChanges();

            return RedirectToAction("IncommingList");
        }
        public ActionResult FriendList()
        {
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            var friendList = ctx.Friends.Where(f => f.Person1 == currentID || f.Person2 == currentID);
            var listOfProfiles = ctx.Profiles.ToList();
            List<FriendListItem> listToSend = new List<FriendListItem>();
            foreach (var f in friendList)
            {
                var profile = listOfProfiles.FirstOrDefault(p => p.UserID != currentID && (p.UserID == f.Person1 || p.UserID == f.Person2));
                var friend = new FriendListItem
                {
                    UserId = profile.UserID,
                    Firstname = profile.FirstName,
                    Lastname = profile.LastName
                };
                listToSend.Add(friend);
            }
            return View(listToSend);
        }
        //Metod för att ta bort person ur vänlista
        public ActionResult RemoveFriend(string friendID)
        {
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            var remove = ctx.Friends.FirstOrDefault(f => f.Person1 == friendID && f.Person2 == currentID || f.Person1 == currentID && f.Person2 == friendID);
            ctx.Friends.Remove(remove);
            ctx.SaveChanges();

            return RedirectToAction("FriendList");
        }
    }
}