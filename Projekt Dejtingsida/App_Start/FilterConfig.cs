using Projekt_Dejtingsida.App_Start;
using System.Web;
using System.Web.Mvc;

namespace Projekt_Dejtingsida
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new FriendRequestCountFilter());
        }
    }
}
