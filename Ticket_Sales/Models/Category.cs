using System.ComponentModel.DataAnnotations;

namespace Ticket_Sales.Models
{
    public class Category
    {
        [Key]
        public int Category_ID { get; set; }
        [Required]
        public string Category_Name { get; set; }
        //public List<Event>? events { get; set; }
    }
}
