using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket_Sales.Models;
using Ticket_Sales.Models.DB;
using Ticket_Sales.Models.Repository;

namespace Ticket_Sales.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LocationController : Controller
    {
        private readonly ILocationRepository _locationRepository;
        private ApplicationDBContext _context;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IActionResult> Index()
        {
            var location = await _locationRepository.GetLocationsAsync();
            return View(location);
        }
        public async Task<IActionResult> Display(string id)
        {
            var location = await _locationRepository.GetLocationByIdAsync(id);
            if (location == null)
                return NotFound();
            return View(location);
        }
        public IActionResult Add() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Add(Location location)
        {
            if (ModelState.IsValid)
            {
                await _locationRepository.AddAsync(location);
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        public async Task<IActionResult> Update(string id)
        {
            var location = await _locationRepository.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, Location location)
        {
            if (id != location.Location_ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _locationRepository.UpdateAsync(location);
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var location = await _locationRepository.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var location = await _locationRepository.GetLocationByIdAsync(id);
            if (location != null)
            {
                await _locationRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
