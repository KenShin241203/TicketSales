using Ticket_Sales.Models;

namespace Ticket_Sales.Models.Repository
{
    public interface ITypeRepository
    {
        Task<IEnumerable<Types>> GetTypesAsync(int eventId);
        Task<IEnumerable<Types>> GetAllTypeAsync();
        Task<Types> GetTypeByIdAsync(int id);
        Task AddAsync(Types type);
        Task UpdateAsync(Types type);
        Task DeleteAsync(int id);
    }
}
