using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Course_project.Data;
using Course_project.Models;
using Course_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Course_project.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using PusherServer;
using Course_project.ViewModels.Items;
using Course_project.Services;
using System.Diagnostics;

namespace Course_project.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;

        public ItemsController(ApplicationDbContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }



        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 15;   
            var count = await _context.Items.CountAsync();
            var items = await _context.Items.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexItemViewModel viewModel = new IndexItemViewModel
            {
                UserId = User.GetUserId(),
                PageViewModel = pageViewModel,
                Items = items
            };
            return View(viewModel);
        }

        public async Task<IActionResult> TagSearch(string id)
        {
                var tag = _context.Tags.FirstOrDefault(m => m.Name == id);
            if (tag != null)
            {
                var tagId = tag.Id;
                var items = await GetItemsForTagSearch(tagId);

                return View(new TagSearchViewModel
                {
                    items = items,
                    tag = tag,
                    userId = User.GetUserId()
                });
            }
            else
            {
                // Обработка случая, когда тег не найден
                // Можно вернуть сообщение об ошибке или выполнить другое действие
                return RedirectToAction("Index", "Home");
            }
        }

		private async  Task<List<Item>> GetItemsForTagSearch(string? tagId)
        {
            var connections = _context.TagConnections.Where(m => m.TagId == tagId);
			var items = new List<Item>();
            int connectionCount = connections.Count();
			List<Item> allItems = await _context.Items.ToListAsync();

			foreach (var con in connections)
			{
				Item i = allItems.FirstOrDefault(m => m.Id == con.ItemId);
				if (i != null)
				{
					items.Add(i);
				}
			}
			return items;
        }


        public async Task<IActionResult> Details(int? id)
        {
            return await GetDetailsAndDeleteViewModel(id);
        }


        private async Task<IActionResult> GetDetailsAndDeleteViewModel(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FirstOrDefaultAsync(m => m.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            var collection = _context.Collections.FirstOrDefault(m => m.Id == item.CollectionId);

            if (collection == null)
            {
                return NotFound();
            }

            var tagList = GetTagsForDetailsPage(id);
            var comments = _context.Comments.Where(m => m.ItemId == id).ToList();
            var userName = (HttpContext == null) ? "name" : HttpContext.User.Identity.Name;
            var response = new DetailsItemViewModel
            {
                comments = comments,
                userName = userName,
                userId = User.GetUserId(),
                collection = collection,
                item = item,
                tags = tagList
            };
            return View(response);
        }

        private List<Tag> GetTagsForDetailsPage(int? itemId)
        {
            var tagConnections = _context.TagConnections.Where(m => m.ItemId == itemId).ToList(); // или .ToListAsync() для асинхронной операции
            var tagList = new List<Tag>();
            if (tagConnections != null)
            {
                foreach (var tag in tagConnections)
                {
                    tagList.Add(_context.Tags.Find(tag.TagId));
                }
            }
            return tagList;
        }

        [Authorize]
        public IActionResult Create(int? CollectionId)
        {
            var response = new CreateItemViewModel(_context.Collections.FirstOrDefault(m => m.Id == CollectionId), new Item());
            var tags = _context.Tags.ToList();
            response.Tags = new List<string>();
            foreach (var tag in tags)
            {
                response.Tags.Add(tag.Name);
            }
            return View(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateItemViewModel item)
        {
            if (ModelState.IsValid)
            {
                item.ThisItem.ImagePath = await _photoService.AddPhotoAsync(item.Image);
                _context.Add(item.ThisItem);
                //_itemsRepository.AddItem(item.ThisItem);S
                await _context.SaveChangesAsync();
                await CreateTags(item.Tags[0], item.ThisItem.Id);
                return RedirectToAction("CollectionItems", "Collection", new { CollectionId = item.ThisItem.CollectionId });
            }

            return View(new CreateItemViewModel(_context.Collections.FirstOrDefault(m => m.Id == item.ThisItem.CollectionId), item.ThisItem));
        }

        private async Task CreateTags(string input, int itemID)
		{
            if (string.IsNullOrEmpty(input))
            {
                return;
            }
            var tags = input.Split(',');
            foreach(var tag in tags)
			{
                var t = await _context.Tags.FirstOrDefaultAsync(m => m.Name == tag);
                if (t == null)
                {
                    t = new Tag { 
                        Id = tag,
                        Name = tag };
                    _context.Tags.Add(t);
                }
                var connection = new TagConnection
                {
                    ItemId = itemID,
                    TagId = t.Id
                };
                _context.TagConnections.Add(connection);
                await _context.SaveChangesAsync();
            }
		}


        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            
            if (item == null)
            {
                return NotFound();
            }

            var collection = await _context.Collections.FindAsync(item.CollectionId);
            
            if (collection == null)
            {
                return NotFound();
            }

            var response = new EditItemViewModel(collection, item, item.Id)
            {
                ImagePath = item.ImagePath
            };
            return View(response);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditItemViewModel input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (input.Image != null)
                    {
                        var result = 
                        input.ThisItem.ImagePath = await _photoService.AddPhotoAsync(input.Image);
                    }

                    _context.Update(input.ThisItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(input.ThisItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Items", new { id = input.ThisItem.Id });
            }
            return View(new EditItemViewModel());
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return await GetDetailsAndDeleteViewModel(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }
            int collectionId = 0;
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                collectionId = item.CollectionId;
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("CollectionItems", "Collection", new { CollectionId = collectionId });
        }

        private bool ItemExists(int id)
        {
          return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
