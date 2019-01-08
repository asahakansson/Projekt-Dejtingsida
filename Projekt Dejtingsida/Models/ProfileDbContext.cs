using System.Data.Entity;

namespace Projekt_Dejtingsida.Models
{
    public class ProfileDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }

        public DbSet<MessageModel> Messages { get; set; }

        public DbSet<FriendRequestModels> FriendRequestModels { get; set; }

        public DbSet<FriendModel> Friends { get; set; }

        public ProfileDbContext() : base("DefaultConnection") { }
    }
}