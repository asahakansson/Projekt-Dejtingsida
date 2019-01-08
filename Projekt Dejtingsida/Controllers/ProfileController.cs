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
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            var profileContext = new ProfileDbContext();
            var userID = User.Identity.GetUserId();
            var currentProfile =
                profileContext.Profiles.FirstOrDefault(p => p.UserID == userID);

            return View(new ProfileViewModels
            {
                UserID = currentProfile.UserID,
                FirstName = currentProfile.FirstName,
                LastName = currentProfile.LastName,
                BirthDate = currentProfile.BirthDate,
                ProfileURL = currentProfile.ProfileURL,
                Description = currentProfile.Description,
                Location = currentProfile.Location
            });
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileViewModels model)
        {
            var profileContext = new ProfileDbContext();
            var userId = User.Identity.GetUserId();
            var currentProfile =
                profileContext.Profiles.FirstOrDefault(p => p.UserID == userId);
            currentProfile.FirstName = model.FirstName;
            currentProfile.LastName = model.LastName;
            currentProfile.BirthDate = model.BirthDate;
            currentProfile.ProfileURL = model.ProfileURL;
            currentProfile.Description = model.Description;
            currentProfile.Location = model.Location;

            profileContext.SaveChanges();

            return RedirectToAction("Index", "Profile");
        }
        [Route("Profile/ShowProfile")]
        [HttpGet]
        public ActionResult ShowProfile(string id)
        {
            var ctx = new ProfileDbContext();
            var userInfo = ctx.Profiles.FirstOrDefault(p => p.UserID == id);
            if(userInfo == null)
            {
                return RedirectToAction("Error", "Profile");
            }
            else { 
            return View(new ProfileViewModels
                {
                    UserID = userInfo.UserID,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    BirthDate = userInfo.BirthDate,
                    ProfileURL = userInfo.ProfileURL,
                    Description = userInfo.Description,
                    Location = userInfo.Location
                });
            }
        }
        [Route("Profile/Error")]
        public ActionResult Error()
        {
            return View();
        }
    }
}