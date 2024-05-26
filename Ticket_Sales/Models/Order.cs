using Microsoft.AspNetCore.Identity;
using Ticket_Sales.Models.DB;

namespace Ticket_Sales.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Email {  get; set; }
        public string eventName {  get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; }
        public string Notes { get; set; }
        public ApplicationUser User { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
    }
}
