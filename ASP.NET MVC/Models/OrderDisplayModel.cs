using DataLibrary.Models;

namespace ASP.NET_MVC.Models
{
    public class OrderDisplayModel
    {
        public OrderModel Order { get; set; }
        public string? ItemPurchased { get; set; }
    }
}
