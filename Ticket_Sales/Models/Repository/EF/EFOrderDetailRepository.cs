using Microsoft.EntityFrameworkCore;
using Ticket_Sales.Models.DB;

namespace Ticket_Sales.Models.Repository.EF
{
    public class EFOrderDetailRepository : IOrderDetailRepository
    {
        public ApplicationDBContext _dbcontext;
        public EFOrderDetailRepository(ApplicationDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IEnumerable<OrderDetail>> GetOrderDetails(int orderId)
        {
            return await _dbcontext.OrderDetail.Where(c => c.OrderId == orderId).ToListAsync();
        }
    }
}
