using Ticket_Sales.Models.DTOs;

namespace Ticket_Sales.Models.Repository
{
    public interface IStockRepository
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = " ");
        Task<Stock?> GetStockByTypeId(int typeId);
        Task ManageStock(StockDTO stockToManage);
    }
}
