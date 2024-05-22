using Microsoft.AspNetCore.Mvc;
using Ticket_Sales.Models.Repository;
using Ticket_Sales.Models.Repository.EF;

namespace Ticket_Sales.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderHistoryController : Controller
    {

        private readonly IOrderRepository _orderRepository;
        public OrderHistoryController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IActionResult> Index(string searching = "")
        {
            var orders = await _orderRepository.GetAllOrder();
            if (searching != null)
            {
                var orderfound = orders.Where(b => b.Email.ToUpper().Contains(searching.ToUpper()));
                return View(orderfound);
            }
            return View(orders);
        }
    }
}
