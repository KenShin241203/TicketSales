using Microsoft.AspNetCore.Mvc.Rendering;
using Ticket_Sales.Models.DB;

namespace Ticket_Sales.Areas.Admin.Models
{
    public class UserRole
    {
        public ApplicationUser applicationUser { get; set; }
        public List<SelectListItem> ApplicationRoles { get; set; }
    }
}
