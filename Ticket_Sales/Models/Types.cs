using System.ComponentModel.DataAnnotations;

namespace Ticket_Sales.Models
{
    public class Types
    {
        [Key]
        public int Type_Id { get; set; }
        [Required, StringLength(128)]
        public string Type_Name { get; set; }
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }
        public decimal TotalPrice => (decimal)Price*orderQuantity;
        public int orderQuantity {  get; set; }
        public int Quantity { get; set; }
        public int EventID { get; set; }
        public string LocationID {  get; set; }
        public Event? events { get; set; }
        public Stock? Stock { get; set; }
    }
}
