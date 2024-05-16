using System.ComponentModel.DataAnnotations;

namespace Ticket_Sales.Areas.Admin.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
