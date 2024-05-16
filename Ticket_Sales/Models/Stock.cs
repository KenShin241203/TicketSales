using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_Sales.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int Id { get; set; }
        public int TypeId {  get; set; }
        public int Quantity {  get; set; }
        public Types? Type { get; set; }
    }
}
