using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ticket_Sales.Models;
using Ticket_Sales.Models.DB;
using Ticket_Sales.Models.Repository;
using Ticket_Sales.Models.Repository.EF;

namespace Ticket_Sales.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TypesController : Controller
    {
        private readonly ITypeRepository _typeRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ApplicationDBContext _context;
        public TypesController(ITypeRepository typeRepository, IEventRepository eventRepository, ILocationRepository locationRepository, ApplicationDBContext context)
        {
            _typeRepository = typeRepository;
            _eventRepository = eventRepository;
            _locationRepository = locationRepository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var types = await _typeRepository.GetAllTypeAsync();
            return View(types);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var type = await _typeRepository.GetTypeByIdAsync(id);
            if (type == null)
                return NotFound();
            return View(type);
        }

        public async Task<IActionResult> Add()
        {
            var events = await _eventRepository.GetEventsAsync();
            ViewBag.Events = new SelectList(events, "Event_ID", "Event_Name");
            var locations = await _locationRepository.GetLocationsAsync();
            ViewBag.Locations = new SelectList(locations, "Location_ID", "City_Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Types type)
        {
            if (ModelState.IsValid)
            {
                await _typeRepository.AddAsync(type);
                return RedirectToAction(nameof(Index));
            }
            var events = await _eventRepository.GetEventsAsync();
            var locations = await _locationRepository.GetLocationsAsync();
            ViewBag.Events = new SelectList(events, "Event_ID", "Event_Name");
            ViewBag.Locations = new SelectList(locations, "Location_ID", "City_Name");
            return View(type);
        }
        public JsonResult GetEventByLocationId(string id )
        {
            List<Event> events = new List<Event>();
            if (id != null)
            {
                events = _context.Event.Where(p => p.LocationID == id).ToList();

            }
            else
            {
                events.Insert(0, new Event { Event_ID = 0, Event_Name = "--Select a event--" });
            }
            var result = (from r in events
                          select new
                          {
                              id = r.Event_ID,
                              name = r.Event_Name
                          }).ToList();

            return Json(result);
        }
        public async Task<IActionResult> Update(int id)
        {
            var type = await _typeRepository.GetTypeByIdAsync(id);
            if (type == null)
            {
                return NotFound();
            }
            var events = await _eventRepository.GetEventsAsync();
            ViewBag.Events = new SelectList(events, "Event_ID", "Event_Name", type.EventID);
            var locations = await _locationRepository.GetLocationsAsync();
            ViewBag.Locations = new SelectList(locations, "Location_ID", "City_Name", type.LocationID);
            return View(type);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Types types)
        {
            if (types.Type_Id != id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingType = await _typeRepository.GetTypeByIdAsync(id);
                existingType.Type_Name = types.Type_Name;
                existingType.Price = types.Price;
                existingType.EventID = types.EventID;
                existingType.LocationID = types.LocationID;
                await _typeRepository.UpdateAsync(existingType);
                return RedirectToAction(nameof(Index));
            }
            var events = await _eventRepository.GetEventsAsync();
            ViewBag.Events = new SelectList(events, "Event_ID", "Event_Name");
            var locations = await _locationRepository.GetLocationsAsync();
            ViewBag.Locations = new SelectList(locations, "Location_ID", "City_Name");
            return View(types);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _typeRepository.GetTypeByIdAsync(id);
            if (type == null)
            {
                return NotFound();
            }
            return View(type);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var type = await _typeRepository.GetTypeByIdAsync(id);
            if (type != null)
            {
                await _typeRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
