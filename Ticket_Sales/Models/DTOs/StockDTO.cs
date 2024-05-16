using System.ComponentModel.DataAnnotations;

namespace Ticket_Sales.Models.DTOs
{
    public class StockDTO
    {
        public int TypeId {  get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int Quantity {  get; set; }
    }
}
