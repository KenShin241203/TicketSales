using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ticket_Sales.Models;
using Ticket_Sales.Models.DB;
using Ticket_Sales.Models.Repository;

namespace Ticket_Sales.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly ApplicationDBContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(IEventRepository eventRepository, ITypeRepository typeRepository, ApplicationDBContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _eventRepository = eventRepository;
            _typeRepository = typeRepository;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }

        public async Task<IActionResult> BookTicket(int id, int quantity =0)
        {
            var events = await _eventRepository.GetEventByIdAsync(id);
            if(events == null)
            {
                return NotFound();
            }
            var cartItem = new CartItem()
            {
                Event = events,
                EventID = events.Event_ID,
                EventName = events.Event_Name,
                Quantity = quantity,
                Types = await _typeRepository.GetTypesAsync(id),
            };

            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.AddEvent(cartItem, cartItem.Types);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Increase(int TypeID)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if(cart != null) 
            {
                var cartItem = cart.Types.FirstOrDefault(x => x.Type_Id == TypeID);
                if(cartItem != null)
                {
                    if(cartItem.orderQuantity <= 9 )
                    {

                        cartItem.orderQuantity++;
                        HttpContext.Session.SetObjectAsJson("Cart", cart);
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Decrease(int TypeID)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null)
            {
                var cartItem = cart.Types.FirstOrDefault(x => x.Type_Id == TypeID);
                if (cartItem.orderQuantity > 0)
                {
                    cartItem.orderQuantity--;
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View(new Order());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            var user = await _userManager.GetUserAsync(User);
            
            order.UserId = user.Id;
            order.OrderDate = DateTime.UtcNow;
            order.TotalPrice = cart.Types.Sum(x => x.Price*x.orderQuantity);
            _dbContext.Order.Add(order);
            await _dbContext.SaveChangesAsync();
            foreach(var item in cart.Types)
            {
                var orderDetail = new OrderDetail
                {
                    TypeId = item.Type_Id,
                    Price = item.Price,
                    OrderId = order.Id,
                    Quantity = item.orderQuantity
                };
                _dbContext.OrderDetail.Add(orderDetail);
                var stock = await _dbContext.Stocks.FirstOrDefaultAsync(a => a.TypeId == item.Type_Id);
                if (stock == null)
                {
                    throw new InvalidOperationException("Stock is null");
                }
                // decrease the number of quantity from the stock table
                stock.Quantity -= item.orderQuantity;
            }
            await _dbContext.SaveChangesAsync();
            HttpContext.Session.Remove("Cart");
            return View("OrderCompleted", order.Id);
        }
    }
}
