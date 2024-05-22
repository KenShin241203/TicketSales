using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Sockets;
using Ticket_Sales.Models;
using Ticket_Sales.Models.DB;
using Ticket_Sales.Models.Repository;
using Ticket_Sales.Models.Repository.EF;

namespace Ticket_Sales.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EventController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ILocationRepository _locationRepository;
        private ApplicationDBContext _context;
        public EventController(IEventRepository eventRepository, ICategoryRepository categoryRepository, ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
            _categoryRepository = categoryRepository;
            _eventRepository = eventRepository;
        }
        public async Task<IActionResult> Index(string? LocationId, string? searchingString)
        {
            LocationId = LocationId ?? null;
            var events = await _eventRepository.GetEventsAsync();
            var locations = await _locationRepository.GetLocationsAsync();
            ViewBag.Locations = new SelectList(locations, "Location_ID", "City_Name", LocationId);
            if (searchingString != null)
            {
                var eventfound = events.Where(x => x.Event_Name.ToUpper().Contains(searchingString.ToUpper()));
                if (LocationId != null)
                {
                    if (LocationId == "TQ")
                    {
                        return View(eventfound);
                    }
                    var locationfound = events.Where(x => x.LocationID == LocationId).Where(x => x.Event_Name.ToUpper().Contains(searchingString.ToUpper()));
                    return View(locationfound);
                }
                return View(eventfound);
            }
            else if (LocationId != null)
            {

                if (LocationId == "TQ")
                {
                    return View(events);
                }
                var locationfound = events.Where(x => x.LocationID == LocationId);
                return View(locationfound);
            }
            return View(events);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            var locations = await _locationRepository.GetLocationsAsync();
            ViewBag.Categories = new SelectList(categories, "Category_ID", "Category_Name");
            ViewBag.Locations = new SelectList(locations, "Location_ID", "City_Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Event Event, IFormFile imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    Event.ImageUrl = await SaveImage(imageUrl);
                }
                await _eventRepository.AddAsync(Event);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var categories = await _categoryRepository.GetCategoriesAsync();
            var locations = await _locationRepository.GetLocationsAsync();
            ViewBag.Categories = new SelectList(categories, "Category_ID", "Category_Name");
            ViewBag.Locations = new SelectList(locations, "Location_ID", "City_Name");
            return View(Event);
        }

        public async Task<IActionResult> Display(int id)
        {
            var Event = await _eventRepository.GetEventByIdAsync(id);
            if (Event == null)
                return NotFound();
            return View(Event);
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/Images", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/Images/" + image.FileName;
        }
        public async Task<IActionResult> Update(int id)
        {
            var Event = await _eventRepository.GetEventByIdAsync(id);
            if (Event == null)
                return NotFound();
            var categories = await _categoryRepository.GetCategoriesAsync();
            var locations = await _locationRepository.GetLocationsAsync();
            ViewBag.Categories = new SelectList(categories, "Category_ID", "Category_Name", Event.CategoryID);
            ViewBag.Locations = new SelectList(locations, "Location_ID", "City_Name", Event.LocationID);
            return View(Event);
        }
        [HttpPost]

        public async Task<IActionResult> Update(int id, Event Event, IFormFile imageUrl)
        {
            if (id != Event.Event_ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingEvent = await _eventRepository.GetEventByIdAsync(id);
                if (imageUrl == null)
                {
                    Event.ImageUrl = existingEvent.ImageUrl;
                }
                else
                {
                    // Lưu hình ảnh mới
                    Event.ImageUrl = await SaveImage(imageUrl);
                }
                existingEvent.Event_Name = Event.Event_Name;
                existingEvent.Date = Event.Date;
                existingEvent.Total_quantity = Event.Total_quantity;
                existingEvent.Address = Event.Address;
                existingEvent.CategoryID = Event.CategoryID;
                existingEvent.LocationID = Event.LocationID;
                await _eventRepository.UpdateAsync(existingEvent);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryRepository.GetCategoriesAsync();
            var locations = await _locationRepository.GetLocationsAsync();
            ViewBag.Categories = new SelectList(categories, "Category_ID", "Category_Name");
            ViewBag.Locations = new SelectList(locations, "Location_ID", "City_Name");
            return View(Event);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var Event = await _eventRepository.GetEventByIdAsync(id);
            if (Event == null)
            {
                return NotFound();
            }
            return View(Event);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eventRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
