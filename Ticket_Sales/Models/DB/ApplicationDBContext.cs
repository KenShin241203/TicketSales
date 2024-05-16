using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticket_Sales.Models;

namespace Ticket_Sales.Models.DB
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Location> Location { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Types> Type { get; set; }
        public DbSet<TicketImages> TicketImages { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Story> Story { get; set; }
        public DbSet<Stock> Stocks { get; set; }

    }
}
