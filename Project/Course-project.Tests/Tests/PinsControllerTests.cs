using CloudinaryDotNet.Actions;
using Course_project.Controllers;
using Course_project.Data;
using Course_project.Models;
using Course_project.Tests.Mocks;
using Course_project.ViewModels;
using Course_project.ViewModels.Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project.Tests.Tests
{
    public class PinsControllerTests : IDisposable
    {
        private static DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("ItemsTest")
    .Options;

        ApplicationDbContext context;

        public PinsControllerTests()
        {
            context = new ApplicationDbContext(dbContextOptions);
            context.Database.EnsureCreated();
            PopulateDb();

        }

        private void PopulateDb()
        {
            var users = FakeData.Users;
            context.Users.AddRange(users);

            var collections = FakeData.Collections;
            context.Collections.AddRange(collections);

            var items = FakeData.Items;
            context.Items.AddRange(items);

            var tags = FakeData.Tags;
            context.Tags.AddRange(tags);

            var tagsConnections = FakeData.TagConnections;
            context.TagConnections.AddRange(tagsConnections);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetItemIndex()
        {
            var controller = new ItemsController(context, null);

            var response = await controller.Index();

            Assert.NotNull(response);
            Assert.IsType<ViewResult>(response);
            var viewResult = (ViewResult)response;
            var model = viewResult.ViewData.Model;
            Assert.IsType<IndexItemViewModel>(model);
            var retVM = (IndexItemViewModel)model;
            Assert.Equal(2, retVM.Items.Count());
        }

        [Fact]
        public void TagSearch()
        {
            var controller = new ItemsController(context, null);

            var response = controller.TagSearch("tag 1");

            Assert.NotNull(response);
            Assert.IsType<ViewResult>(response);
            var viewResult = (ViewResult)response;
            var model = viewResult.ViewData.Model;
            Assert.IsType<TagSearchViewModel>(model);
            var retVM = (TagSearchViewModel)model;
            Assert.Equal(1, retVM.items.Count());
        }

        [Fact]
        public async Task GetItemDetails()
        {
            var controller = new ItemsController(context, null);

            var response = await controller.Details(1);

            Assert.NotNull(response);
            Assert.IsType<ViewResult>(response);
            var viewResult = (ViewResult)response;
            var model = viewResult.ViewData.Model;
            Assert.IsType<DetailsItemViewModel>(model);
            var retVM = (DetailsItemViewModel)model;
            Assert.Equal("Item 1", retVM.item.Name);
        }

        [Fact]
        public async Task CreateItem_Ok()
        {
            var fakeService = new FakePhotoService();
            fakeService.Mock.Setup(s => s.AddPhotoAsync(null))
                .Returns(Task.FromResult("ssilka"));
            var controller = new ItemsController(context, fakeService.PhotoService);
            var item = new Item
            {
                Name = "Test",
                AuthorId = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8").ToString(),
                IntCustom1 = 1,
                IntCustom2 = 2,
                IntCustom3 = 3,
            };
            var sentVm = new CreateItemViewModel
            {
                ThisItem = item,
                Image = null,
                ThisCollection = new Collection
                {
                    AuthorId = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8").ToString(),
                },
                Tags = new List<string> { "super" },
            };
            var response = await controller.Create(sentVm);

            Assert.NotNull(response);
            Assert.IsType<RedirectToActionResult>(response);
            Assert.Equal(3, context.Items.Count());
        }

        [Fact]
        public async Task EditItem()
        {
            var fakeService = new FakePhotoService();
            fakeService.Mock.Setup(s => s.AddPhotoAsync(null))
                .Returns(Task.FromResult("ssilka"));
            var controller = new ItemsController(context, fakeService.PhotoService);
            var item = new Item
            {
                Name = "Test",
                AuthorId = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8").ToString(),
                IntCustom1 = 1,
                IntCustom2 = 2,
                IntCustom3 = 3,
            };
            var sentVm = new EditItemViewModel
            {
                ThisItem = item,
                Image = null,
                ThisCollection = new Collection
                {
                    AuthorId = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8").ToString(),
                },
            };
            var response = await controller.Edit(sentVm);

            Assert.NotNull(response);
            Assert.IsType<RedirectToActionResult>(response);
        }


        [Fact]
        public async Task DeleteItem_NotFound()
        {
            var controller = new ItemsController(context, null);
            var response = await controller.Delete(111);

            Assert.NotNull(response);
            Assert.IsType<NotFoundResult>(response);
        }


        [Fact]
        public async Task DeleteItem_Ok()
        {
            var controller = new ItemsController(context, null);
            int? id = 1;
            var response = await controller.Delete(id);

            Assert.NotNull(response);
            Assert.IsType<RedirectToActionResult>(response);
            Assert.Equal(1, context.Items.Count());
        }

        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        public void Dispose()
        {
            CleanUp();
        }
    }
}
