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
        // Startsidan för profil. Skapar en ProfileViewModel här för att kunna skicka vidare till våra Partials.
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
        // Söksidan
        [HttpGet]
        public ActionResult Search(string firstname, string lastname, string location)
        {
            ViewBag.Message = "Search page.";
            // Vi har 3 sök kriterar som vi använder av oss av.
            // Användare med förinställda för och efternamn syns inte i resultet.
            var currentUserID = User.Identity.GetUserId();
            var Profiles = new ProfileDbContext().Profiles.Where(
                    (s =>
                    (s.FirstName.Contains(firstname) || firstname == null && !(s.FirstName == "Firstname")) &&
                    (s.LastName.Contains(lastname) || lastname == null && !(s.LastName == "Lastname")) &&
                    (s.Location.Contains(location) || location == null) &&
                    !(s.UserID.Equals(currentUserID))));
            return View(Profiles);
        }
        //Metod som sparar profiluppgifter i databasen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProfileViewModels model)
        {
            //Vi kontrollerar att födelsedatumet ligger inom spannet 1900 till i år.
            if (model.BirthDate.Year < 1900 || model.BirthDate.Year > DateTime.Now.Year)
            {
                ModelState.AddModelError("BirthDate", "Date not valid");
                return View(model);
            }
            var profileContext = new ProfileDbContext();
            var userId = User.Identity.GetUserId();
            var currentProfile =
                profileContext.Profiles.FirstOrDefault(p => p.UserID == userId);
            currentProfile.FirstName = model.FirstName;
            currentProfile.LastName = model.LastName;
            currentProfile.BirthDate = model.BirthDate;
            currentProfile.Description = model.Description;
            currentProfile.Location = model.Location;

            profileContext.SaveChanges();

            return RedirectToAction("ShowProfile", "Profile", new { showID = userId });
        }
        // Visar profilen
        [HttpGet]
        public ActionResult ShowProfile(string showID)
        {
            // Tar in ett ID och använder detta för att visa information om användaren.
            var ctx = new ProfileDbContext();
            var userInfo = ctx.Profiles.FirstOrDefault(p => p.UserID == showID);
            // Kontroll att ID är korrekt.
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
        // Error view.
        public ActionResult Error()
        {
            return View();
        }
        // För att redigera användardata.
        public ActionResult Edit()
        {
            var profileContext = new ProfileDbContext();
            var userID = User.Identity.GetUserId();
            var showProfile =
                profileContext.Profiles.FirstOrDefault(p => p.UserID == userID);
            //Data skickas till vyn för att uppdateringsformuläret ska bli ifyllt med det som finns i databasen
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
        //Metod för att byta profilbild
        public ActionResult ChangePicture(HttpPostedFileBase File)
        {
            //Vi kollar att det finns en bild att spara
            if (File != null && File.ContentLength > 0)
            {
                //Hämtar filnamnet utan filändelse
                var NoExtension = Path.GetFileNameWithoutExtension(File.FileName);
                //Hämtar filändelsen
                var Extension = Path.GetExtension(File.FileName);
                //Lägger ihop de båda, men med nuvarande tidpukt i namnet.
                //Det görs för att filnamnet ska bli unikt.
                var NameOfFile = NoExtension + DateTime.Now.ToString("yyyy-MM-dd-fff") + Extension;
                //Filen där bilderna sparas och filnamnet slås ihop till en sträng
                var NameOfPath = "/Images/" + NameOfFile;
                string FilePath = Path.Combine(Server.MapPath("~/Images/"), NameOfFile);
                //Bilden sparas
                File.SaveAs(FilePath);

                var pdb = new ProfileDbContext();
                var userId = User.Identity.GetUserId();
                var currentProfile =
                    pdb.Profiles.FirstOrDefault(p => p.UserID == userId);
                //Bildens sökväg sparas i databasen
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