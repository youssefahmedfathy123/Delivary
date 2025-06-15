using System.Text.RegularExpressions;
using Tawsel.Enums.ProductEnum;

namespace Tawsel.Models
{
    public class Product :BaseEntity<int>
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string ImageUrl { get; set; }
        public ProductDepartment Department { get; set; }
        public List<Buy> Buys { get; set; }
        public List<Favourite> Favourites { get; set; }

    }
}


