using System.ComponentModel.DataAnnotations.Schema;
using Tawsel.Enums.Status;

namespace Tawsel.Models
{
    public class Status : BaseEntity<int>
    {
        [ForeignKey("Buy")]
        public int BuyId { get; set; }
        public Buy Buy { get; set; }
        public BuysStatus BuyStatus { get; set; }
    }
}



