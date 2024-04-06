using Microsoft.AspNetCore.Identity;

namespace Course_project.ViewModels
{
    public class IndexAdminViewModel
    {
        public List<IdentityUser> Users { get; set; }

        public List<string> Roles { get; set; }
    }
}
