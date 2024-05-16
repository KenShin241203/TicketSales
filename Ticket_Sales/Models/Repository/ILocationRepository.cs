using Ticket_Sales.Models;

namespace Ticket_Sales.Models.Repository
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetLocationsAsync();
        Task<Location> GetLocationByIdAsync(string id);
        Task AddAsync(Location location);
        Task DeleteAsync(string id);
        Task UpdateAsync(Location location);
    }
}
