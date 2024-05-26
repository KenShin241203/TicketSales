using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ticket_Sales.Models.DB;
using Ticket_Sales.Models.Repository;

namespace Ticket_Sales.Controllers
{
    public class UserOrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDBContext _dbContext;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public UserOrderController(IOrderRepository orderRepository, UserManager<ApplicationUser> userManager, ApplicationDBContext dbContext, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _dbContext = dbContext;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<IActionResult> Index()
        {
         
            var userId = _userManager.GetUserId(User);
            var userOrders = await _orderRepository.GetOrderByUserId(userId); 
            return View(userOrders);
        }
        [HttpPost]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            var orderDetail = await _orderDetailRepository.GetOrderDetails(order.Id);
            if(orderDetail == null)
            {
                return NotFound();
            }
            return View(orderDetail);
        }
    }
}
