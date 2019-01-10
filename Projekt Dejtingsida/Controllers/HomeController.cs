using Projekt_Dejtingsida.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Projekt_Dejtingsida.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var Profiles = new ProfileDbContext().Profiles.ToList().Take(3);
            return View(Profiles);
        }

        public ActionResult About()
        {
            ViewBag.Message = "About us";

			return View();
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
    }
}