using System.ComponentModel.DataAnnotations.Schema;
using Tawsel.Enums.Delivary;
using Tawsel.Models;

namespace Tawsel.ViewModels
{
    public class BuyViewModel
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public Day Day { get; set; }
        public DateOnly Date { get; set; }
        public Hour Hour { get; set; }
        public string Address { get; set; }


    }
}


