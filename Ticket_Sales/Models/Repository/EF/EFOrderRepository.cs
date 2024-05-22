
using Microsoft.EntityFrameworkCore;
using Ticket_Sales.Models.DB;

namespace Ticket_Sales.Models.Repository.EF
{
    public class EFOrderRepository : IOrderRepository
    {
        public ApplicationDBContext _dbcontext;
        public EFOrderRepository(ApplicationDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            return await _dbcontext.Order.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrderByEmail(string email)
        {
            return await _dbcontext.Order.Where(a => a.Email == email).ToListAsync();
        }
    }
}
