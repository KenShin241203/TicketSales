
using Microsoft.EntityFrameworkCore;
using Ticket_Sales.Models;
using Ticket_Sales.Models.DB;
using Ticket_Sales.Models.Repository;

namespace Ticket_Sales.Models.Repository.EF
{
    public class EFLocationRepository : ILocationRepository
    {
        private readonly ApplicationDBContext _context;

        public EFLocationRepository(ApplicationDBContext context) { _context = context; }

        public async Task<IEnumerable<Location>> GetLocationsAsync()
        {
            return await _context.Location.ToListAsync();
        }
        public async Task<Location> GetLocationByIdAsync(string id)
        {
            return await _context.Location.FirstOrDefaultAsync(x => x.Location_ID == id);
        }

        public async Task AddAsync(Location location)
        {
            _context.Location.Add(location);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var locattion = await _context.Location.FindAsync(id);
            _context.Location.Remove(locattion);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Location location)
        {
            _context.Location.Update(location);
            await _context.SaveChangesAsync();
        }
    }
}
