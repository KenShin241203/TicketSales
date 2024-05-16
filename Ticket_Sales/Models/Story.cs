namespace Ticket_Sales.Models
{
    public class Story
    {
        public int Id { get; set; }
        public int? OrderID { get; set; }
        public Order? order { get; set; }
    }
}
