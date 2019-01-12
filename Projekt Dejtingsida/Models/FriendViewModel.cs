using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt_Dejtingsida.Models
{
    public class FriendViewModel
    {
        public List<FriendModel> FriendModels { get; set; }
    }
    public class FriendListItem
    {
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

    }
    public class FriendLists
    {
        public List<FriendListItem> FriendList { get; set; }
    }
}