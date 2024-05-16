namespace Ticket_Sales.Models.DTOs
{
    public class StockDisplayModel
    {
        public int Id { get; set; }
        public int TypeId {  get; set; }
        public string? TypeName {  get; set; }
        public int EventId {  get; set; }
        public int Quantity {  get; set; }
        public Event? events { get; set; }
    }
}
