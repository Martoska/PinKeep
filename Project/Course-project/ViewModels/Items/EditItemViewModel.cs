using Course_project.Models;

namespace Course_project.ViewModels.Items
{
    public class EditItemViewModel
    {
        public int ItemId { get; set; }

        public string? ImagePath { get; set; }

        public Item ThisItem { get; set; }

        public Collection? ThisCollection { get; set; }

        public IFormFile? Image { get; set; }

        public EditItemViewModel(Collection collection, Item item, int itemId)
        {
            ThisItem = item;
            ThisCollection = collection;
            ItemId = itemId;
        }

        public EditItemViewModel()
        {
        }
    }
}
