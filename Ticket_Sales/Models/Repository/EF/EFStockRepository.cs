using Microsoft.EntityFrameworkCore;
using Ticket_Sales.Models.DB;
using Ticket_Sales.Models.DTOs;

namespace Ticket_Sales.Models.Repository.EF
{
    public class EFStockRepository :IStockRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public EFStockRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Stock?> GetStockByTypeId(int typeId)
        {
            return await _dbContext.Stocks.FirstOrDefaultAsync(p => p.TypeId == typeId);
        }
        public async Task ManageStock(StockDTO stockToManage)
        {
            var existingStock = await GetStockByTypeId(stockToManage.TypeId);
            if (existingStock is null)
            {
                var stock = new Stock { TypeId = stockToManage.TypeId, Quantity = stockToManage.Quantity };
                _dbContext.Stocks.Add(stock);
            }
            else
            {
                existingStock.Quantity = stockToManage.Quantity;
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "")
        {
            var stocks = await (from types in _dbContext.Type
                                join stock in _dbContext.Stocks
                                on types.Type_Id equals stock.TypeId
                                into type_stock
                                from typeStock in type_stock.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sTerm) || types.events.Event_Name.ToLower().Contains(sTerm.ToLower())
                                select new StockDisplayModel
                                {
                                    TypeId = types.Type_Id,
                                    TypeName = types.Type_Name,
                                    EventId = types.EventID,
                                    events = types.events,
                                    Quantity = typeStock == null ? 0 : typeStock.Quantity,
                                }).ToListAsync();
            return stocks;
        }
    }
}
