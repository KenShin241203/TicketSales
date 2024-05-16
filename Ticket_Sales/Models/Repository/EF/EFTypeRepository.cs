
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using Ticket_Sales.Models;
using Ticket_Sales.Models.DB;
using Ticket_Sales.Models.Repository;

namespace Ticket_Sales.Models.Repository.EF
{
    public class EFTypeRepository : ITypeRepository
    {
        private readonly ApplicationDBContext _context;

        public EFTypeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Types>> GetAllTypeAsync()
        {
            return await _context.Type.ToListAsync();
        }
        public async Task<IEnumerable<Types>> GetTypesAsync(int eventId)
        {
           IEnumerable<Types> types = await(from type in _context.Type 
                                            join events in _context.Event
                                            on type.EventID equals events.Event_ID
                                            join stock in _context.Stocks
                                            on type.Type_Id equals stock.TypeId 
                                            into type_stocks from typeWithStock in type_stocks.DefaultIfEmpty()
                                            where (type != null)
                                            select new Types
                                            {
                                                Type_Id = type.Type_Id,
                                                Type_Name = type.Type_Name,
                                                EventID = type.EventID,
                                                LocationID = type.LocationID,
                                                Price = type.Price,
                                                Quantity = typeWithStock == null ? 0 : typeWithStock.Quantity,
                                            }
                                            ).ToListAsync();
            if(eventId > 0)
            {
                types = types.Where(x => x.EventID == eventId).ToList();
            }
            return types;
        }

        public async Task<Types> GetTypeByIdAsync(int id)
        {
            return await _context.Type.Include(p => p.events).FirstOrDefaultAsync(p => p.Type_Id == id);
        }
        public async Task AddAsync(Types Type)
        {
            _context.Type.Add(Type);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var type = await _context.Type.FindAsync(id);
            _context.Type.Remove(type);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Types Type)
        {
            _context.Type.Update(Type);
            await _context.SaveChangesAsync();
        }


    }
}
