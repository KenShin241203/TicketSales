using Ticket_Sales.Models.DB;

namespace Ticket_Sales.Models.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrder();
        Task<IEnumerable<Order>> GetOrderByEmail(string email);
        Task<IEnumerable<Order>> GetOrderByUserId(string userId);
        Task<Order> GetOrderById(int orderId);
        
    }
}
