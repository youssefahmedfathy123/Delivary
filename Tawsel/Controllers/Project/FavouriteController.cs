using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tawsel.Data;
using Tawsel.Models;

namespace Tawsel.Controllers.Project
{
    public class FavouriteController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FavouriteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Add(int id)
        {
            var UserId = HttpContext.User.GetUserId();
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            var favourite = new Favourite
            {
                UserId = UserId,
                ProductId = id
            };
            await _context.Favourites.AddAsync(favourite);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index" ,"Product");

        }

        public async Task<IActionResult> Show(int PageNumber = 1)
        {
            var UserId = HttpContext.User.GetUserId();
            var table = await _context.Favourites
                .Include(x => x.User)
                .Include(x => x.Product)
                .Where(x => x.UserId == UserId)
                .Skip(2*(PageNumber - 1)).Take(2)
                .ToListAsync();

            return View(table ?? new List<Favourite>());
        }




    }
}


