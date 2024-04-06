using Course_project.Models;

namespace Course_project.ViewModels.Items
{
    public class TagSearchViewModel
    {

        public string? userId { get; set; }

        public Tag tag { get; set; }

        public List<Item> items { get; set; }
    }
}
