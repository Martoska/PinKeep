using Course_project.Models;
using Microsoft.AspNetCore.Identity;

namespace Course_project.ViewModels.Account
{
    public class UserPageViewModel
    {
        public string UserName { get; set; }
        public IEnumerable<Collection> Collections { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
