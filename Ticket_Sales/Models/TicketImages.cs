using System.ComponentModel.DataAnnotations;

namespace Ticket_Sales.Models
{
    public class TicketImages
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
