using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tawsel.Enums.Delivary;

namespace Tawsel.Models
{
    public class Buy : BaseEntity<int>
    {
        public User User { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public Day Day { get; set; }
        public DateOnly Date { get; set; }
        public Hour Hour { get; set; }
        public string Address { get; set; }
        public List<Delivary> Delivaries { get; set; }
        public List<Status> Statuses { get; set; }
        
    }
}




