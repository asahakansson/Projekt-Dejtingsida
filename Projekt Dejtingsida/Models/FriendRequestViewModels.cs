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
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

    }
    public class RequestList
    {
        public List<FriendRequestList> RequestLists { get; set; }
    }
    public class PendingRequests
    {
        public int Outgoing { get; set; }
        public int Incomming { get; set; }
    }
}