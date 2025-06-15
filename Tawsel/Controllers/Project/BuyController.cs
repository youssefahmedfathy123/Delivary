using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tawsel.Data;
using Tawsel.Models;
using Tawsel.ViewModels;

namespace Tawsel.Controllers.Project
{
    public class BuyController : Controller
    {

        private readonly ApplicationDbContext _context;
        public BuyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Add(int ProductId)
        {
            var UserId = HttpContext.User.GetUserId();

            var x = new BuyViewModel
            {
                ProductId = ProductId,
                UserId = UserId
            };

            return View(x);
        }


        [HttpPost]
        public async Task<IActionResult> Add(BuyViewModel buy)
        {
            var NewObj = new Buy
            {
                UserId = buy.UserId,
                ProductId = buy.ProductId,
                Quantity = buy.Quantity,
                Day = buy.Day,
                Hour = buy.Hour,
                Date = buy.Date,
                Address = buy.Address
            };

            await _context.Buys.AddAsync(NewObj);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index","Product");
        }



    }
}



