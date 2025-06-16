using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tawsel.Data;
using Tawsel.Enums.Delivary;
using Tawsel.Enums.Status;
using Tawsel.Enums.User;
using Tawsel.Interfaces;
using Tawsel.Models;
using Tawsel.Repository;
using Tawsel.ViewModels.Admin;

namespace Tawsel.Controllers.Admin
{
    public class Show : Controller
    {
        private readonly IRepository<Buy> _repos;
        private readonly IRepository<Status> _reposStatus;
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Delivary> _reposDel; 


        public Show(IRepository<Buy> repos, ApplicationDbContext context, IRepository<Delivary> reposDel, IRepository<Status> reposStatus)
        {
            _repos = repos;
            _context = context;
            _reposDel = reposDel;
            _reposStatus = reposStatus;
        }

        public async Task<IActionResult> GetBuyTable(int PageNumber = 1)
        {
            var result = await _context.Buys.Include(x => x.Product).Include(x => x.User).Skip(2 * (PageNumber - 1)).Take(2)
                .ToListAsync();

            return View(result);
        }

        public async Task<IActionResult> GetBuyTableByDayAndDate()
        {
            return View(new GetBuyTableByDayAndDateVM());
        }

        [HttpPost]
        public IActionResult GetBuyTableByDayAndDate(GetBuyTableByDayAndDateVM model)
        {
            return RedirectToAction("ShowToday", new {date = model.Date , day = model.Day });
        }

        public async Task<IActionResult> ShowToday(DateOnly date ,Day day , int PageNumber = 1)
        {
                    var result = await _context.Buys
                        .Include(x => x.Product)
                        .Include(x => x.User)
                        .Where(x => x.Day == day && x.Date == date)
                        .Skip(2*(PageNumber - 1)).Take(2)
                        .ToListAsync();

                    ViewBag.x = PageNumber;
                    return View(result ?? new List<Buy>());
        }

        public async Task<IActionResult> GetDelivaryMen(int PageNumber = 1)
        {
            var delivaryMen = await _context.Users
                .Where(x => x.Role == Role.Delivary).Skip(2 * (PageNumber - 1)).Take(2)
                .ToListAsync();
            return View(delivaryMen);
        }

        public async Task<IActionResult> SetDelivaryMan(string id)
        {
            var x = new SetDelivaryManVM
            {
                DelivaryManId = id
            };

            return View(x);
        }

        [HttpPost]
        public async Task<IActionResult> SetDelivaryMan(SetDelivaryManVM model)
        {
            var newTable = new Delivary
            {
                DelivaryManId = model.DelivaryManId,
                BuyId = model.BuyId
            };
            await _reposDel.Add(newTable);
            await _reposDel.Save();

            return RedirectToAction("GetDelivaryMen");

        }

        public async Task<IActionResult> GetDelivaryTable()
        {
            return View(new GetBuyTableByDayAndDateVM());
        }

        [HttpPost]
        public IActionResult GetDelivaryTable(GetBuyTableByDayAndDateVM model)
        {
            return RedirectToAction("ShowDelivaryTable", new { day = model.Day, date = model.Date });
        }

        public async Task<IActionResult> ShowDelivaryTable(DateOnly date, Day day,int PageNumber = 1)
        {

                var del = await _context.Delivaries
                .Include(x => x.User)
                .Include(x => x.Buy)
                .ThenInclude(b => b.Product)
                .Where(x => x.Buy.Day == day && x.Buy.Date == date)
                .Skip(2 * (PageNumber - 1)).Take(2)
                .ToListAsync();

                ViewBag.x = PageNumber;

                return View(del ?? new List<Delivary>());

         }

        public async Task<IActionResult> SetStatus(int id)
        {
            return View(
               new SetStatusViewModel
            {
                BuyId = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> SetStatus(SetStatusViewModel model)
        {
            var newTable = new Status
            {
                BuyId = model.BuyId,
                BuyStatus = model.BuysStatus
            };

            await _reposStatus.Add(newTable);
            await _reposStatus.Save();

            return RedirectToAction("GetBuyTable");

        }

        public async Task<IActionResult> BuysTableByStatus()
        {
            return View(new BuyTableByStatusVM());
        }

        [HttpPost]
        public async Task<IActionResult> BuysTableByStatus(BuyTableByStatusVM model)
      {
          return RedirectToAction("BuysByStatus", new { status = model.BuysStatus });
      }
        
        public async Task<IActionResult> BuysByStatus(BuysStatus status, int PageNumber = 1)
      {
              var result = await _context.Statuses
                  .Include(x => x.Buy)
                  .ThenInclude(x => x.Product)
                  .Include(x => x.Buy)
                  .ThenInclude(x => x.User)
                  .Where(x => x.BuyStatus == status)
                  .Skip(2*(PageNumber - 1))
                  .Take(2)
                  .ToListAsync();
                  
              ViewBag.x = PageNumber;
          return View(result ?? new List<Status>() );
             }        
          }
      }







//امتى تستخدم كل واحدة؟
//استخدم الـ Query String (زي الطريقة الأولى):
//لو عايز البيانات تكون جزء من الـ URL وتفضل موجودة لو المستخدم عمل ريفريش أو شارك الرابط.
//لو البيانات بسيطة (زي status بس) ومش حساسة.
//لو عايز دعم التصفح الصفحي (زي في الطريقة الأولى) عشان الـ URL بيحتفظ بالمعلومات.
//استخدم TempData (زي الطريقة الثانية):
//لو البيانات معقدة أو حساسة ومش عايزها تظهر في الـ URL.
//لو البيانات دي مجرد خطوة وسيطة وهتستخدم مرة واحدة (زي تاريخ ويوم معين).
//لو عايز الـ URL يفضل قصير ونظيف.
//الفرق باختصار
//الطريقة الأولى بتمرر البيانات في الـ URL (Query String)، فهي أوضح وأكثر دوامًا، بس ممكن تكون مش مناسبة لو البيانات حساسة.
//الطريقة الثانية بتستخدم TempData، فالبيانات بتبقى مخفية ومؤقتة، بس ممكن تضيع لو السيشن اتقطع أو المستخدم عمل ريفريش.

// Tempdata way

//public async Task<IActionResult> GetDelivaryTable()
//{
//    return View(new GetBuyTableByDayAndDateVM());
//}

//[HttpPost]
//public IActionResult GetDelivaryTable(GetBuyTableByDayAndDateVM model)
//{
//    TempData["Day"] = model.Day.ToString();
//    TempData["Date"] = model.Date.ToString();

//    return RedirectToAction("ShowDelivaryTable");
//}

//public async Task<IActionResult> ShowDelivaryTable()
//{
//    if (TempData["Day"] is string dayString && TempData["Date"] is string dateString)
//    {
//        if (Enum.TryParse<Day>(dayString, out var day) && DateOnly.TryParse(dateString, out var date))
//        {
//            var result = await _context.Delivaries
//                .Include(x => x.User)
//                .Include(x => x.Buy)
//                .ThenInclude(b => b.Product)
//                .Where(x => x.Buy.Day == day && x.Buy.Date == date)
//                .ToListAsync();
//            return View(result);
//        }
//    }
//    return View(new List<Delivary>());

//}
