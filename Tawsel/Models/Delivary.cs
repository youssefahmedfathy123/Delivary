using System.ComponentModel.DataAnnotations.Schema;

namespace Tawsel.Models
{
    public class Delivary : BaseEntity<int>
    {

        [ForeignKey("User")]
        public string DelivaryManId { get; set; }
        public User User { get; set; }

        [ForeignKey("Buy")]
        public int BuyId { get; set; }
        public Buy Buy { get; set; }
        

    }
}


