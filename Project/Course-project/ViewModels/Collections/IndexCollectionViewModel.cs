using Course_project.Models;

namespace Course_project.ViewModels.Collections
{
    public class IndexCollectionViewModel
    {
        public IEnumerable<Collection> Collections { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
