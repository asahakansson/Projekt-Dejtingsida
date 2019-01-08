using Projekt_Dejtingsida.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }
        [HttpGet]
		public ActionResult Search(string firstname, string lastname, string location) 
		{
			ViewBag.Message = "Search page.";

            var Profiles = new ProfileDbContext().Profiles.Where(
                    (s => (s.FirstName.Contains(firstname) || firstname == null) &&
                    (s.LastName.Contains(lastname) || lastname == null) && 
                    (s.Location.Contains(location) || location == null))
                ).ToList();
            return View(Profiles);
		}
    }
}