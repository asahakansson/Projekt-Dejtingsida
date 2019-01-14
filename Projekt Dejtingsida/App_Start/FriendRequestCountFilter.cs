using Microsoft.AspNet.Identity;
using Projekt_Dejtingsida.Models;
using System.Linq;
using System.Web.Mvc;

namespace Projekt_Dejtingsida.App_Start
{
    public class FriendRequestCountFilter : ActionFilterAttribute
    {
        public FriendRequestCountFilter()
        {
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if(filterContext.HttpContext.User?.Identity.IsAuthenticated ?? false) //Om användaren är inloggad är det sant, annars falskt
            {
                //Sätter IncomingRequestCount till antalet vänförfrågningar användaren har
                var ctx = new ProfileDbContext();
                var currentID = filterContext.HttpContext.User.Identity.GetUserId();
                var incommingRequests = ctx.FriendRequestModels.Where(f => f.Person2 == currentID);
                var req = incommingRequests.Count();
                filterContext.Controller.ViewData["IncomingRequestsCount"] = req;
            }
        }
    }
}