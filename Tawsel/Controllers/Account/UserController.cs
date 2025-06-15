using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tawsel.Data;
using Tawsel.Interfaces;
using Tawsel.Models;
using Tawsel.ViewModels;

namespace Tawsel.Controllers.Account
{
    public class UserController : Controller
    {

        private readonly UserManager<User> _user;
        private readonly SignInManager<User> _sign;
        private readonly IPhotoServices _photo;


        public UserController(UserManager<User> user, SignInManager<User> sign, IPhotoServices photo)
        {
            _user = user;
            _sign = sign;
            _photo = photo;
        }

        public IActionResult Registeration()
        {
            RegisterViewModel reg = new();
            return View(reg);

        }

        [HttpPost]
        public async Task<IActionResult> Registeration(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var find1 = await _user.FindByEmailAsync(register.Email);
            var find2 = await _user.FindByNameAsync(register.Username);

            if(find1 != null || find2 != null)
                return BadRequest("Login!");

            var ph = await _photo.AddPhoto(register.Image);
            if (ph == null)
                return BadRequest("Photo not uploaded");

            var NewUser = new User
            {
                UserName = register.Username,
                Email = register.Email,
                FullName = register.Name,
                PhoneNumber = register.Phone,
                ImageUrl = ph?.Url.ToString(),
                Role = register.Role
            };

            var creation = await _user.CreateAsync(NewUser, register.Password);
            if (!creation.Succeeded)
            {
                var errors = creation.Errors.Select(x => x.Description).ToList();
                return BadRequest(errors);
            }
            var role = await _user.AddToRoleAsync(NewUser,register.Role.ToString());
            if (!role.Succeeded)
            {
                var errors = role.Errors.Select(x => x.Description).ToList();
                return BadRequest(errors);
            }
            await _sign.SignInAsync(NewUser, isPersistent: false);


            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Login()
        {
            LoginViewModel log = new();
            return View(log);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return StatusCode(500,ModelState);

            var find1 = await _user.FindByEmailAsync(login.Email);
            if(find1 == null)
                return NotFound("Register?");

            var result = await _sign.CheckPasswordSignInAsync(find1, login.Password!, false);

            if (!result.Succeeded)
                return Unauthorized(new { message = "Incorrect password" });


            await _sign.SignInAsync(find1, isPersistent: false);

            return RedirectToAction("Index","Home"); 

        }


        public async Task<IActionResult> Logout()
        {
            await _sign.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

    }
}


