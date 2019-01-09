using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt_Dejtingsida.Models
{
    public class FriendRequestViewModels
    {
        public List<FriendRequestModels> FriendRequestModels { get; set; }
    }
    public class RequestSent
    {
        public bool Success { get; set; }
        public string Error { get; set; }
    }
    public class FriendRequestList
    {
        public string ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

    }
    public class RequstList
    {
        public List<FriendRequestList> FriendRequestLists { get; set; }
    }
}