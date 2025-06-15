using Tawsel.Enums.ProductEnum;

namespace Tawsel.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public IFormFile Image { get; set; }
        public ProductDepartment Department { get; set; }

    }
}


