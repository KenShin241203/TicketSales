using System.ComponentModel.DataAnnotations;

namespace Ticket_Sales.Models
{
    public class Location
    {
        [Key]
        public string Location_ID { get; set; }
        [Required]
        public string City_Name { get; set; }
    }
}
