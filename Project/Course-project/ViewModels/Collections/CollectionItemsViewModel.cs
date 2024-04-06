using Course_project.Models;

namespace Course_project.ViewModels.Collections
{
    public class CollectionItemsViewModel
    {
        public string UserId { get; set; }
        public List<Item> Items { get; set; }

        public Collection ThisCollection { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public CollectionItemsViewModel(List<Item> items, Collection collection, PageViewModel pageViewModel, string userId)
        {
            Items = items;
            ThisCollection = collection;
            PageViewModel = pageViewModel;
            UserId = userId;
        }
    }
}
