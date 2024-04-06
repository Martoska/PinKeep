using System.Security.Claims;
using Course_project.Data;
using Course_project.Services;
using Course_project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Course_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {

            int pageSize = 15;
            var collection = _context.Collections.ToList();
            var count = await _context.Items.CountAsync();
            var items = await _context.Items.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            HomeViewModel viewModel = new HomeViewModel
            {
                UserId = User.GetUserId(),
                Collections = collection.Take(5).Reverse().ToList(),
                Tags = _context.Tags.ToList(),
                PageViewModel = pageViewModel,
                Items = Enumerable.Reverse(items).ToList()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> CreateAdminRole()
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = UserRoles.Admin,
                NormalizedName = "ADMIN"
            };
            _context.Roles.Add(identityRole);
            IdentityRole identityRole1 = new IdentityRole
            {
                Name = UserRoles.User,
                NormalizedName = "USER"
            };
            _context.Roles.Add(identityRole1);
            await _context.SaveChangesAsync();
            return Content("ok");
        }
    }
}
