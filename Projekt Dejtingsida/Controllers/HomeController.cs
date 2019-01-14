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
        // Startsidan. 
        public ActionResult Index()
        {
            // Visar 3 användare från start.
            var Profiles = new ProfileDbContext().Profiles.ToList().Take(3);
            return View(Profiles);
        }

        public ActionResult About()
        {
            ViewBag.Message = "About us";

			return View();
        }
    }
}