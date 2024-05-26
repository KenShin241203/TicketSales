namespace Ticket_Sales.Models.Repository
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetOrderDetails(int orderId);
    }
}
