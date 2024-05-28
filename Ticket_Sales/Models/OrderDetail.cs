using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_Sales.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int TypeId { get; set; }
        public string TypeName {  get; set; }
        public int EventId {  get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Order? Order { get; set; }
        public Types? Type { get; set; }
    }
}
