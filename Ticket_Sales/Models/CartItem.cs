namespace Ticket_Sales.Models
{
    public class CartItem
    {
        public int EventID {  get; set; }
        public int TypeID {  get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set;}
        public decimal Price {  get; set; }
        public int Quantity {  get; set; }
        public string? ImageUrl { get; set; }
        public List<TicketImages>? Images { get; set; }
        public Event? Event { get; set; }
        public Types? types { get; set; }
        public IEnumerable<Types> Types {  get; set; } 
    }
}
