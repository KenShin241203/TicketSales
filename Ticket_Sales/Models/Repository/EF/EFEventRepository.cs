
using Microsoft.EntityFrameworkCore;
using Ticket_Sales.Models;
using Ticket_Sales.Models.DB;
using Ticket_Sales.Models.Repository;

namespace Ticket_Sales.Models.Repository.EF
{
    public class EFEventRepository : IEventRepository
    {
        private readonly ApplicationDBContext _context;
        public EFEventRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await _context.Event.Include(p => p.category).
                Include(q => q.location).ToListAsync();
        }
        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Event.Include(p => p.category)
                .Include(q => q.location).FirstOrDefaultAsync(x => x.Event_ID == id);
        }

        public async Task AddAsync(Event Event)
        {
            _context.Event.Add(Event);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var events = await _context.Event.FindAsync(id);
            _context.Event.Remove(events);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Event events)
        {
            _context.Event.Update(events);
            await _context.SaveChangesAsync();
        }


    }
}
