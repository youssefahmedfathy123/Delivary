using Tawsel.Enums.ProductEnum;

namespace Tawsel.ViewModels
{
    public class DeleteProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Salary { get; set; }
        public string? ImageUrl { get; set; }
        public ProductDepartment? Department { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

    }
}


