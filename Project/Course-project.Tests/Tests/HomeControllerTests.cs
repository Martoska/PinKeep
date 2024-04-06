using Course_project.Controllers;
using Course_project.Data;
using Course_project.Models;
using Course_project.Tests.Mocks;
using Course_project.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Course_project.Tests.Tests
{
    public class HomeControllerTests : IDisposable
    {

        private static DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("HomeTest")
            .Options;

        ApplicationDbContext context;

        public HomeControllerTests()
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
            context.SaveChanges();
        }

        [Fact]
        public async Task GetHomeIndex()
        {
            var controller = new HomeController(context);

            var response = await controller.Index();

            Assert.NotNull(response);
            Assert.IsType<ViewResult>(response);
            var viewResult = (ViewResult)response;
            var model = viewResult.ViewData.Model;
            Assert.IsType<HomeViewModel>(model);
            var retVM = (HomeViewModel)model;
            Assert.Equal(2, retVM.Items.Count());
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
