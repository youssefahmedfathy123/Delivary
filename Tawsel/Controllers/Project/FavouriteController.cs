using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tawsel.Data;
using Tawsel.Interfaces;
using Tawsel.Models;

namespace Tawsel.Controllers.Project
{
    public class FavouriteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Favourite> _repo;
        public FavouriteController(ApplicationDbContext context, IRepository<Favourite> repo)
        {
            _context = context;
            _repo = repo;
        }

        public async Task<IActionResult> Add(int id,int PageNumber)
        {
            var UserId = HttpContext.User.GetUserId();
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            var existed = await _context.Favourites
                .FirstOrDefaultAsync(x => x.UserId == UserId && x.ProductId == id);

            if(existed == null)
            {
                var favourite = new Favourite
                {
                    UserId = UserId,
                    ProductId = id
                };
                await _repo.Add(favourite);
                await _repo.Save();
            }

            return RedirectToAction("Index" ,"Product",new { PageNumber = PageNumber });

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

            ViewBag.CurrentPage = PageNumber;
            return View(table ?? new List<Favourite>());
        }

        public async Task<IActionResult> Remove(int id,int PageNumber)
        {
            var UserId = HttpContext.User.GetUserId();
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            var existed = await _context.Favourites
                .FirstOrDefaultAsync(x => x.UserId == UserId && x.ProductId == id);

            if (existed != null)
            {
                await _repo.Delete(existed);
                await _repo.Save();
            }

            return RedirectToAction("Show", new {PageNumber = PageNumber});

        }



    }
}


