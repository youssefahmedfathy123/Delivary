using System.ComponentModel.DataAnnotations.Schema;

namespace Tawsel.Models
{
    public class Favourite : BaseEntity<int>
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
}


