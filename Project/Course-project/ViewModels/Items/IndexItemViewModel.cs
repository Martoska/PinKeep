using Course_project.Models;

namespace Course_project.ViewModels.Items
{
    public class IndexItemViewModel
    {
        public string UserId { get; set; }
        public List<Item> Items { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}
