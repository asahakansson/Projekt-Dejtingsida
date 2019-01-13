using Microsoft.AspNet.Identity;
using Projekt_Dejtingsida.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            var showProfile =
                profileContext.Profiles.FirstOrDefault(p => p.UserID == userID);

            return View(new ProfileViewModels
            {
                UserID = showProfile.UserID,
                FirstName = showProfile.FirstName,
                LastName = showProfile.LastName,
                BirthDate = showProfile.BirthDate,
                ProfileURL = showProfile.ProfileURL,
                Description = showProfile.Description,
                Location = showProfile.Location
            });
        }
        [HttpGet]
        public ActionResult Search(string firstname, string lastname, string location)
        {
            ViewBag.Message = "Search page.";
            var currentUserID = User.Identity.GetUserId();
            var Profiles = new ProfileDbContext().Profiles.Where(
                    (s =>
                    (s.FirstName.Contains(firstname) || firstname == null && !(s.FirstName == "Firstname")) &&
                    (s.LastName.Contains(lastname) || lastname == null && !(s.LastName == "Lastname")) &&
                    (s.Location.Contains(location) || location == null) &&
                    !(s.UserID.Equals(currentUserID))));
            return View(Profiles);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProfileViewModels model)
        {
            if (model.BirthDate.Year < 1900 || model.BirthDate.Year > DateTime.Now.Year)
            {
                ModelState.AddModelError("BirthDate", "No");
                return View(model);
            }
            var profileContext = new ProfileDbContext();
            var userId = User.Identity.GetUserId();
            var currentProfile =
                profileContext.Profiles.FirstOrDefault(p => p.UserID == userId);
          
            currentProfile.FirstName = model.FirstName;
            currentProfile.LastName = model.LastName;
            currentProfile.BirthDate = model.BirthDate;
            //currentProfile.ProfileURL = model.ProfileURL;
            currentProfile.Description = model.Description;
            currentProfile.Location = model.Location;

            profileContext.SaveChanges();

            return RedirectToAction("ShowProfile", "Profile", new { showID = userId });
        }

        [Route("Profile/ShowProfile")]
        [HttpGet]
        public ActionResult ShowProfile(string showID)
        {
            var ctx = new ProfileDbContext();
            var userInfo = ctx.Profiles.FirstOrDefault(p => p.UserID == showID);
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

        public ActionResult Edit()
        {
            var profileContext = new ProfileDbContext();
            var userID = User.Identity.GetUserId();
            var showProfile =
                profileContext.Profiles.FirstOrDefault(p => p.UserID == userID);

            return View(new ProfileViewModels
            {
                UserID = showProfile.UserID,
                FirstName = showProfile.FirstName,
                LastName = showProfile.LastName,
                BirthDate = showProfile.BirthDate,
                ProfileURL = showProfile.ProfileURL,
                Description = showProfile.Description,
                Location = showProfile.Location
            });

        }

        public ActionResult ChangePicture(HttpPostedFileBase File)
        {

            if (File != null && File.ContentLength > 0)
            {
                var NoExtension = Path.GetFileNameWithoutExtension(File.FileName);
                var Extension = Path.GetExtension(File.FileName);
                var NameOfFile = NoExtension + DateTime.Now.ToString("yyyy-MM-dd-fff") + Extension;
                var NameOfPath = "/Images/" + NameOfFile;
                string FilePath = Path.Combine(Server.MapPath("~/Images/"), NameOfFile);
                File.SaveAs(FilePath);

                var pdb = new ProfileDbContext();
                var userId = User.Identity.GetUserId();
                var currentProfile =
                    pdb.Profiles.FirstOrDefault(p => p.UserID == userId);
                currentProfile.ProfileURL = NameOfPath;
                pdb.SaveChanges();

                return RedirectToAction("ShowProfile", "Profile", new { showID = userId });
               
            }
            else
            {
                return RedirectToAction("Error", "Profile");
            }
        }
    }
}