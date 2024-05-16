using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Ticket_Sales.Models
{
    public class Event
    {
        [Key]
        public int Event_ID { get; set; }
        [Required]
        public string Event_Name { get; set; }
        public DateTime Date { get; set; }
        public int Total_quantity { get; set; }
        public string Address { get; set; }
        public int CategoryID { get; set; }
        public string LocationID { get; set; }
        public string? ImageUrl { get; set; }
        public Category? category { get; set; }
        public Location? location { get; set; }
        public List<TicketImages>? Images { get; set; }
    }
}
