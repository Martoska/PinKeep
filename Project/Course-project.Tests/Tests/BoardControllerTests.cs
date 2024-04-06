using Course_project.Controllers;
using Course_project.Data;
using Course_project.Tests.Mocks;
using Course_project.ViewModels.Collections;
using Course_project.ViewModels.Items;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project.Tests.Tests
{
    public class BoardControllerTests : IDisposable
    {
        private static DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("CollectionsTest")
            .Options;

        ApplicationDbContext context;

        public BoardControllerTests()
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
        public async Task GetCollectionIndex_Ok_WithData()
        {
            var controller = new CollectionController(context);

            var response = await controller.Index(Models.Enums.CollectionType.Books);

            Assert.NotNull(response);
            Assert.IsType<ViewResult>(response);
            var viewResult = (ViewResult)response;
            var model = viewResult.ViewData.Model;
            Assert.IsType<IndexCollectionViewModel>(model);
            var retVM = (IndexCollectionViewModel)model;
            Assert.Equal(2, retVM.Collections.Count());
        }


        [Fact]
        public async Task GetCollectionIndex_Ok_WithoutData()
        {
            var controller = new CollectionController(context);

            var response = await controller.Index(Models.Enums.CollectionType.NFTs);

            Assert.NotNull(response);
            Assert.IsType<ViewResult>(response);
            var viewResult = (ViewResult)response;
            var model = viewResult.ViewData.Model;
            Assert.IsType<IndexCollectionViewModel>(model);
            var retVM = (IndexCollectionViewModel)model;
            Assert.Empty(retVM.Collections);
        }

        [Fact]
        public async Task GetCollectionItems_Ok_WithData()
        {
            var controller = new CollectionController(context);

            var response = await controller.CollectionItems(1);

            Assert.NotNull(response);
            Assert.IsType<ViewResult>(response);
            var viewResult = (ViewResult)response;
            var model = viewResult.ViewData.Model;
            Assert.IsType<CollectionItemsViewModel>(model);
            var retVM = (CollectionItemsViewModel)model;
            Assert.Single(retVM.Items);
        }


        [Fact]
        public async Task GetCollectionItems_NotFound()
        {
            var controller = new CollectionController(context);
            var response = await controller.CollectionItems(111);

            Assert.NotNull(response);
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task CreateCollection_Ok()
        {
            var controller = new CollectionController(context);
            var model = new CreateCollectionViewModel
            {
                Name = "Foo",
                Type = Models.Enums.CollectionType.NFTs,
                Description = "Bar",
                StringName1 = "Foo1",
                IntName1 = "suk"
            };
            var response = await controller.Create(model);

            Assert.NotNull(response);
            Assert.IsType<RedirectToActionResult>(response);
            var redirectResult = (RedirectToActionResult)response;
            Assert.Equal("Account", redirectResult.ControllerName);
            Assert.Equal("UserPage", redirectResult.ActionName);
            Assert.Equal(3, context.Collections.Count());
        }


        [Fact]
        public void DeleteCollection_Ok()
        {
            var controller = new CollectionController(context);
            int? id = 1;
            var response = controller.Delete(id);

            Assert.NotNull(response);
            Assert.IsType<RedirectToActionResult>(response);
            var redirectResult = (RedirectToActionResult)response;
            Assert.Equal("Account", redirectResult.ControllerName);
            Assert.Equal("UserPage", redirectResult.ActionName);
            Assert.Equal(1, context.Collections.Count());
        }


        [Fact]
        public void DeleteCollection_NotFound()
        {
            var controller = new CollectionController(context);
            int? id = 111;
            var response = controller.Delete(id);

            Assert.NotNull(response);
            Assert.IsType<NotFoundResult>(response);
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
