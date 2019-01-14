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
        // Startsidan för vänlistan/kontaktlistan.
        // Hämtar info om det finns inkommande eller utgående förfrågningar.
        public ActionResult Index()
        {
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            var incommingRequests = ctx.FriendRequestModels.Where(f => f.Person2 == currentID);
            var outgoingrequests = ctx.FriendRequestModels.Where(f => f.Person1 == currentID);
            var pending = new PendingRequests { Incomming = incommingRequests.Count(), Outgoing = outgoingrequests.Count() };
            return View(pending);
        }
        // Används för att skicka vänförfrågningar.
        // Flera olika valideringar sker.
        [HttpGet]
        public ActionResult RequestList(string friendID)
        {
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            // Kontrollerar att ID inte är tomt och att den användaren existerar.
            var friendExist = ctx.Profiles.Any(p => p.UserID == friendID);
            if ((!(friendID == null)) && friendExist)
            {
                // Kontrollerar om det inte finns vänförfrågningar sen innan.
                if (!(ctx.FriendRequestModels.Any(f =>
                (f.Person1 == currentID && f.Person2 == friendID) ||
                (f.Person1 == friendID && f.Person2 == currentID)
                )))
                {
                    // Kontrollerar att användarna inte är vänner sen innan.
                    if (!(ctx.Friends.Any(f =>
                     (f.Person1 == currentID && f.Person2 == friendID) ||
                     (f.Person1 == friendID && f.Person2 == currentID))))
                    {
                        // Vid lyckat resultat, skapas en FriendRequestModel och läggs in i databasen.
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
        // Visar inkommande vänförfrågningar.
        public ActionResult IncommingList()
        {
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            var listOfRequests = ctx.FriendRequestModels.Where(f => f.Person2 == currentID);
            var listOfProfiles = ctx.Profiles.ToList();
            List<FriendRequestList> listToSend = new List<FriendRequestList>();
            // Hämtar förnamn och efternamn på användarens inkommande vänförfrågningar och skickar dessa i en lista.
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
            // Kontrollerar att användaren har några vänner/kontakter.
            if (listToSend.Any())
            {
                return View(listToSend);
            }
            else
            {
                return View();
            }
        }
        // Visar skickade vänförfrågningar
        public ActionResult OutgoingList()
        {
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            var listOfRequests = ctx.FriendRequestModels.Where(f => f.Person1 == currentID);
            var listOfProfiles = ctx.Profiles.ToList();
            List<FriendRequestList> listToSend = new List<FriendRequestList>();
            // Hämtar förnamn och efternamn på användarens skickade vänförfrågningar och skickar dessa i en lista.
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
            // Kontrollerar att de finns några skickade vänförfrågningar, annars skickas ingen info vidare.
            if (listToSend.Any())
            {
                return View(listToSend);
            }
            else
            {
                return View();
            }
        }
        // Ta bort skickade vänförfrågningar.
        public ActionResult RemoveRequest(int removeID)
        {
            // Använder inkommande ID på vänförfrågan för att ta bort denna.
            var ctx = new ProfileDbContext();
            var remove = ctx.FriendRequestModels.FirstOrDefault(f => f.Id == removeID);
            ctx.FriendRequestModels.Remove(remove);
            ctx.SaveChanges();
            // Skickar vidare en till ens skickade vänförfrågningar.
            return RedirectToAction("OutgoingList");
        }
        // Hanterar den respons användaren gör på vänförfrågan.
        public ActionResult RequestRespone(int requestID, bool acceptRequest)
        {
            var ctx = new ProfileDbContext();
            var friendRequest = ctx.FriendRequestModels.FirstOrDefault(f => f.Id == requestID);
            // Vid accapterande av vänförfrågan.
            if (acceptRequest)
            {
                var addFriend = new FriendModel { Person1 = friendRequest.Person1, Person2 = friendRequest.Person2 };
                ctx.Friends.Add(addFriend);
                ctx.SaveChanges();
            }
            // Sen tas den bort, vilket sker i båda fallen (accept/decline).
            var remove = ctx.FriendRequestModels.FirstOrDefault(f => f.Id == requestID);
            ctx.FriendRequestModels.Remove(remove);
            ctx.SaveChanges();
            // Skickas tillbaka till inkommande förfrågningar.
            return RedirectToAction("IncommingList");
        }
        // Hämtar användarens vänlista.
        public ActionResult FriendList()
        {
            var ctx = new ProfileDbContext();
            var currentID = User.Identity.GetUserId();
            var friendList = ctx.Friends.Where(f => f.Person1 == currentID || f.Person2 == currentID);
            var listOfProfiles = ctx.Profiles.ToList();
            List<FriendListItem> listToSend = new List<FriendListItem>();
            // Hämtar förnamn, efternamn och ID't på alla kontakter som användaren har.
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
            // Skickar sedan vidare kontakterna i en lista.
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