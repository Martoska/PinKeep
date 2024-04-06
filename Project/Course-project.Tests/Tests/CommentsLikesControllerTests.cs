using Course_project.Controllers;
using Course_project.Data;
using Course_project.Tests.Mocks;
using Course_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ContentResult = Microsoft.AspNetCore.Mvc.ContentResult;

namespace Course_project.Tests.Tests
{
    public class CommentsLikesControllerTests : IDisposable
    {

        private static DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("CommentsLikesTest")
            .Options;

        ApplicationDbContext context;

        public CommentsLikesControllerTests()
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
        public void GetLikesCount()
        {
            var controller = new CommentsLikesController(context);

            var response = controller.GetLikesCount(1);

            Assert.NotNull(response);
            Assert.IsType<ContentResult>(response);
            var okResult = response as ContentResult;
            Assert.Equal(0, Convert.ToInt32(okResult.Content));
        }

        [Fact]
        public async Task PostComment()
        {
            var controller = new CommentsLikesController(context);
            var comment = "asd||asd||asd||1";

            var response = await controller.PostComment(comment);

            Assert.NotNull(response);
            Assert.IsType<ContentResult>(response);
            var okResult = response as ContentResult;
            Assert.Equal("ok", Convert.ToString(okResult.Content));
        }


        private void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        public void Dispose()
        {
            CleanUp();
        }
    }
}
