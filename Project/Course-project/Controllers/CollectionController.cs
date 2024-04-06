using System.Security.Claims;
using Course_project.Data;
using Course_project.Helpers;
using Course_project.Models;
using Course_project.Models.Enums;
using Course_project.Services;
using Course_project.ViewModels;
using Course_project.ViewModels.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Course_project.Controllers
{

    public class CollectionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollectionController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(CollectionType? type, int page = 1,
                    SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Collection> collections = _context.Collections;

            var viewModelOptions = new ViewModelOptions()
            {
                Collections = collections
            }.GetSortedAndFilteredCollection(type,page, sortOrder);
            
            IndexCollectionViewModel viewModel = new IndexCollectionViewModel
            {
                PageViewModel = new PageViewModel(viewModelOptions.Result.Count, page, viewModelOptions.Result.PageSizeCollection),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(_context.Collections.ToList(), type, nameof(type)),
                Collections = viewModelOptions.Result.Items
            };
            return View(viewModel);
        }

        public async Task<IActionResult> CollectionItems(int? CollectionId, int page = 1)
        {
            return await GetDetailsAndDeleteViewModel(CollectionId, page);
        }

        private async Task<IActionResult> GetDetailsAndDeleteViewModel(int? CollectionId, int page = 1)
        {
            var collection = await _context.Collections.Where(s => s.Id== CollectionId).FirstOrDefaultAsync();
            if (collection == null)
            {
                return NotFound();
            }
            int pageSize = 15;
            List<Item> items = new List<Item>();
            var count = await _context.Items.Where(m => m.CollectionId == CollectionId).CountAsync();
            if (count <= pageSize)
            {
                items = await _context.Items.Where(e => e.CollectionId == CollectionId).ToListAsync();
            }
            else
            {
                items = await _context.Items.Skip((page - 1) * pageSize).Take(pageSize).Where(e => e.CollectionId == CollectionId).ToListAsync();
            }
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            var response = new CollectionItemsViewModel(items, _context.Collections.FirstOrDefault(m => m.Id == CollectionId), pageViewModel, User.GetUserId());
            return View(response);
        }

        [Authorize]
        public IActionResult Create()
        {
            var response = new CreateCollectionViewModel();
            return View(response);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id, int page = 1)
        {
            return await GetDetailsAndDeleteViewModel(id, page);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var collection = _context.Collections.FirstOrDefault(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }
            var items = _context.Items.Where(m => m.CollectionId == collection.Id).ToList();
            foreach (var item in items)
            {
                _context.Items.Remove(item);
            }
            _context.Collections.Remove(collection);
            _context.SaveChanges();

            return RedirectToAction("UserPage", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCollectionViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var collection = CreateCollectionFromViewModel(input);
            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserPage", "Account");
        }

        private Collection CreateCollectionFromViewModel(CreateCollectionViewModel input)
        {
            return new Collection
            {
                AuthorId = User.GetUserId(),
                Name = input.Name,
                Type = input.Type,
                Description = input.Description,
                IntName1 = input.IntName1,
                IntName2 = input.IntName2,
                IntName3 = input.IntName3,
                StringName1 = input.StringName1,
                StringName2 = input.StringName2,
                StringName3 = input.StringName3,
                DateName1 = input.DateName1,
                DateName2 = input.DateName2,
                DateName3 = input.DateName3,
                BoolName1 = input.BoolName1,
                BoolName2 = input.BoolName2,
                BoolName3 = input.BoolName3,
                LargeStringName1 = input.LargeStringName1,
                LargeStringName2 = input.LargeStringName2,
                LargeStringName3 = input.LargeStringName3
            };
        }
    }
}
