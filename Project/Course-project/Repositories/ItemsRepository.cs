using Course_project.Data;
using Course_project.Interfaces;
using Course_project.Models;

namespace Course_project.Repositories
{
	public class ItemsRepository : IItemsRepository
	{

		private ApplicationDbContext context;

		public ItemsRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public void AddItem(Item item)
		{
			context.Items.Add(item);
		}

		public void DeleteItem(int itemtId)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public Item GetItemById(int Id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Item> GetItems()
		{
			throw new NotImplementedException();
		}

		public void Save()
		{
			throw new NotImplementedException();
		}

		public void UpdateItem(Item item)
		{
			throw new NotImplementedException();
		}
	}
}
