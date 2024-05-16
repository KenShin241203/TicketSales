using Ticket_Sales.Models;

namespace Ticket_Sales.Models.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddAsync(Category category);
        Task DeleteAsync(int id);
        Task UpdateAsync(Category category);
    }
}
