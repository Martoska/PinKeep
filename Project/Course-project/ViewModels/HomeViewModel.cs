using Course_project.Models;

namespace Course_project.ViewModels
{
    public class HomeViewModel
    {
        public string UserId { get; set; }
        public List<Tag> Tags { get; set; }

        public List<Item> Items { get; set; }

        public List<Collection> Collections { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}
