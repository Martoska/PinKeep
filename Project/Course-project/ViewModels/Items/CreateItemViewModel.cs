using Course_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Course_project.ViewModels.Items
{
    public class CreateItemViewModel
    {
        public Item ThisItem { get; set; }

        public List<string> Tags { get; set; }

        public string? InputedTags { get; set; }

        public Collection? ThisCollection { get; set; }

        public IFormFile Image { get; set; }

        public CreateItemViewModel(Collection collection, Item item)
        {
            ThisItem = item;
            ThisCollection = collection;
        }

        public CreateItemViewModel()
        {
        }
    }
}
