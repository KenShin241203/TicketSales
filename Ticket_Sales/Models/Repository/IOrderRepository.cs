namespace Ticket_Sales.Models.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrder();
        Task<IEnumerable<Order>> GetOrderByEmail(string email);
    }
}
