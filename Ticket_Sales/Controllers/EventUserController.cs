using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Ticket_Sales.Models.DB;
using Ticket_Sales.Models.Repository;

namespace Ticket_Sales.Controllers
{
    public class EventUserController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly ILocationRepository _locationRepository;
        ApplicationDBContext _dbContext;
        public EventUserController(IEventRepository eventRepository, ApplicationDBContext context, ILocationRepository locationRepository)
        {
            _eventRepository = eventRepository;
            _dbContext = context;
            _locationRepository = locationRepository;
        }
       
        public async Task<IActionResult> Music(string? LocationId, string? searchingString)
        {
            LocationId = LocationId??null;
            var events = _dbContext.Event.Where(p=>p.CategoryID == 1).ToList();
            var locations = await _locationRepository.GetLocationsAsync();
            ViewBag.Locations = new SelectList(locations, "Location_ID", "City_Name", LocationId);
            if (searchingString != null)
            {
                var eventfound = events.Where(x => x.Event_Name.ToUpper().Contains(searchingString.ToUpper()));
                if ( LocationId != null)
                {
                    if(LocationId == "TQ")
                    {
                        return View(eventfound);
                    }
                    var locationfound = events.Where(x => x.LocationID == LocationId).Where(x => x.Event_Name.ToUpper().Contains(searchingString.ToUpper()));
                    return View(locationfound);
                }
                return View(eventfound);
            }
            else if(LocationId != null)
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

        public async Task<IActionResult> Arts(string? LocationId, string? searchingString)
        {
            LocationId = LocationId ?? null;
            var events = _dbContext.Event.Where(p => p.CategoryID == 3).ToList();
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
        public async Task<IActionResult> Sports(string? LocationId, string? searchingString)
        {
            LocationId = LocationId ?? null;
            var events = _dbContext.Event.Where(p => p.CategoryID == 2).ToList();
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
    }
}
