using Course_project.Models;

namespace Course_project.ViewModels.Items
{
    public class DetailsItemViewModel
    {
        public Item item { get; set; }

        public Collection collection { get; set; }

        public List<Tag> tags { get; set; }

        public List<Comment> comments { get; set; }

        public string? userName { get; set; }

        public string? userId { get; set; }

        public string? search { get; set; }
    }
}
