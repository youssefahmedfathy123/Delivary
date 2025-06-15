using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Tawsel.Data;
using Tawsel.Enums.ProductEnum;
using Tawsel.Interfaces;
using Tawsel.Models;
using Tawsel.ViewModels;

namespace Tawsel.Controllers.Project
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly IPhotoServices _photo;
        private readonly ApplicationDbContext _context;

        public ProductController(IRepository<Product> repository, IPhotoServices photo, ApplicationDbContext context)
        {
            _repository = repository;   
            _photo = photo;
            _context = context;
        }


        public async Task<IActionResult> Index(int PageNumber = 1)
        {            
            var gets = await _context.Products.Skip(2*(PageNumber -1)).Take(2)
                .ToListAsync();

            return View(gets);

        }


        public IActionResult Create()
        {
            var product = new ProductViewModel();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel newProduct)
        {
            if (!ModelState.IsValid)
                return StatusCode(500, new {Error = "Invalis model state!"});


            var photo = await _photo.AddPhoto(newProduct.Image);
            if (photo == null)
                return BadRequest("Photo not uploaded successfully!");


         var product = new Product
            {
                Department = newProduct.Department,
                ImageUrl = photo?.Url.ToString(),
                Name = newProduct.Name,
                Salary = newProduct.Salary
            };

            await _repository.Add(product);
            await _repository.Save();

            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Edit(int id)
        {
            var table = await _context.Products.FindAsync(id);
            if (table is null)
                return NotFound("Table not found!");

            var editTable = new EditViewModel
            {
                Id = id,
                Department = table.Department,
                Name = table.Name,  
                Salary = table.Salary
            };

            return View(editTable);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var table = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == product.Id);

            try
            {
                await _photo.DeletePhoto(table.ImageUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            var photo = await _photo.AddPhoto(product.Image);
            if (photo is null)
                return BadRequest("Photo not uploaded!");

            var newPro = new Product
            {
                Id = product.Id,
                Department = product.Department,
                ImageUrl = photo.Url.ToString(),
                Name = product.Name,
                Salary = product.Salary,
                CreatedAt = table.CreatedAt,
                CreatedBy = table.CreatedBy,
            };

            _repository.Edit(newPro);
            await _repository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var table = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(table is null)
                return NotFound("Table not found");

            var send = new DeleteProductViewModel
            {
                Id = table.Id,
                Department = table.Department,
                Name = table.Name,
                Salary = table.Salary,
                CreatedAt = table.CreatedAt,
                CreatedBy = table.CreatedBy,
                ImageUrl = table.ImageUrl,
            };

            return View(send);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteProductViewModel delete)
        {
            var table = await _context.Products.FindAsync(delete.Id);
            
            _repository.Delete(table);

            await _repository.Save();
            return RedirectToAction("Index");
        }

    }
}



