using Ticket_Sales.Models;

namespace Ticket_Sales.Models.Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task AddAsync(Event events);
        Task DeleteAsync(int id);
        Task UpdateAsync(Event events);
    }
}
