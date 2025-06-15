using Microsoft.AspNetCore.Identity;
using Tawsel.Enums.User;
using Tawsel.Models;

namespace Tawsel.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }
        public IFormFile Image { get; set; }


    }
}


