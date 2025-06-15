using Microsoft.AspNetCore.Identity;
using Tawsel.Enums.User;

namespace Tawsel.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public Role Role { get; set; }
        public string ImageUrl { get; set; }
        public List<Buy> Buys { get; set; }
        public List<Delivary> Delivaries { get; set; }
        public List<Favourite> Favourites { get; set; }

    }
}



