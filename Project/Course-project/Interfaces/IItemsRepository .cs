using Course_project.Models;

namespace Course_project.Interfaces
{
    public interface IItemsRepository : IDisposable
    {
        IEnumerable<Item> GetItems();
        Item GetItemById(int Id);
        void AddItem(Item item);
        void DeleteItem(int itemtId);
        void UpdateItem(Item item);
        void Save();
    }
}
