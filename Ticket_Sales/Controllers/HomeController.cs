using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Ticket_Sales.Models;
using Ticket_Sales.Models.DB;
using Ticket_Sales.Models.Repository;

namespace Ticket_Sales.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDBContext _dbContext;

        public HomeController(IEventRepository eventRepository, ITypeRepository typeRepository,ICategoryRepository categoryRepository, ApplicationDBContext dbContext)
        {
            _eventRepository = eventRepository;
            _typeRepository = typeRepository;
            _categoryRepository = categoryRepository;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var events = await _eventRepository.GetEventsAsync();

            return View(events);
        }

        public async Task<IActionResult> Detail(int id)
        {
            CartItem cartObj = new()
            {
             
                Event = await _eventRepository.GetEventByIdAsync(id),
                Types = await _typeRepository.GetTypesAsync(id),
                
            };
            return View(cartObj);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
