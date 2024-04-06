using Course_project.Data;
using Course_project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Course_project.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var responce = GetUserTable().Result;
            return View(responce);
        }

        private async Task<IndexAdminViewModel> GetUserTable()
        {
            var users = _context.Users.ToList();
            var roles = new List<string>();
            for (int i = 0; i < users.Count(); i++)
            {
                var role = await _userManager.GetRolesAsync(users[i]);
                if (role.Count() != 0)
                {
                    if (role[0] == "admin")
                    {
                        roles.Add(role[0]);
                        continue;
                    }
                    roles.Add("user");
                    continue;
                }
                roles.Add("user");
            }

            var responce = new IndexAdminViewModel()
            {
                Roles = roles,
                Users = users
            };
            return responce;
        }

        public async Task<IActionResult> Block(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTime.UtcNow.AddYears(100);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> Unblock(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.LockoutEnabled = false;
                user.LockoutEnd = DateTime.UtcNow;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> ChangeRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var role = await _userManager.GetRolesAsync(user);
                if (role.Count() == 0)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    return RedirectToAction("Index", "Admin");
                }
                if (role[0] == "admin")
                {
                    await _userManager.RemoveFromRoleAsync(user, "admin");
                    await _userManager.AddToRoleAsync(user, "user");
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, "user");
                    await _userManager.AddToRoleAsync(user, "admin");
                }

            }
            return RedirectToAction("Index", "Admin");
        }

		public async Task<IActionResult> Delete(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				var collections = _context.Collections.Where(m => m.AuthorId == id).ToList();
				foreach (var c in collections)
				{
					await DeleteCollection(c.Id);
				}
				_context.Users.Remove(user);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction("Index", "Admin");
		}

		private async Task DeleteCollection(int id)
		{
			var collection = _context.Collections.FirstOrDefault(m => m.Id == id);
			if (collection != null)
			{
				var items = _context.Items.Where(m => m.CollectionId == collection.Id).ToList();
				foreach (var item in items)
				{
					_context.Items.Remove(item);
				}
				_context.Collections.Remove(collection);
				await _context.SaveChangesAsync();
			}
		}
	}
}
