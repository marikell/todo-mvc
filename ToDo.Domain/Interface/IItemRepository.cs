using ToDo.Domain.Entities;

namespace ToDo.Domain.Interface;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllAsync();
    Task AddAsync(Item item);
    Task EditAsync(Item item);
    Task<Item> GetById(Guid id);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Guid id, Item item);
}