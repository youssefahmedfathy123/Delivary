using Tawsel.Enums.ProductEnum;

namespace Tawsel.ViewModels
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public IFormFile Image { get; set; }
        public ProductDepartment Department { get; set; }

    }
}

