using System.Diagnostics;
using System.Security.Claims;
using Course_project.Data;
using Course_project.Models;
using Course_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Course_project.Controllers
{
    public class CommentsLikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsLikesController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult GetLikesCheck(int id)
        {
            var userId = User.GetUserId();
            var likes = _context.Likes.Where(m => m.ItemId == id).ToList();
            var userLike = likes.FirstOrDefault(m => m.UserId == userId);
            bool hasLiked = (userLike != null);
            return Json(hasLiked);
        }

        public IActionResult GetLikesCount(int id)
		{
            var count = _context.Likes.Where(m => m.ItemId == id).ToList().Count();
            return Content(count.ToString());
        }


		public IActionResult OnLikeClick(int id)
		{
			var userId = User.GetUserId();

			if (string.IsNullOrEmpty(userId))
			{
				return BadRequest("User not authenticated.");
			}

			var userLike = _context.Likes.FirstOrDefault(m => m.ItemId == id && m.UserId == userId);
			var count = _context.Likes.Count(m => m.ItemId == id);

			if (userLike == null)
			{
				var like = new Like
				{
					UserId = userId,
					ItemId = id
				};
				_context.Likes.Add(like);
				count++; // Увеличиваем значение count после добавления лайка
			}
			else
			{
				_context.Likes.Remove(userLike);
				count--; // Уменьшаем значение count после удаления лайка
			}

			_context.SaveChanges();

			return Ok(count);
		}

		[HttpPost]
        public async Task<IActionResult> PostComment(string msg)
        {
            var vars = msg.Split("||");
            var message = new Comment
            {
                Message = vars[0],
                UserId = User.GetUserId(),
                UserName = vars[2],
                ItemId = int.Parse(vars[3]),
                DateTime = DateTime.Now,
            };
            _context.Comments.Add(message);
            await _context.SaveChangesAsync();

            return Content("ok");
        }

        public IActionResult GetStyle()
        {
            if (User.Identity.IsAuthenticated)
            {
                var connection = _context.StyleConnections.FirstOrDefault(m => m.UserId == User.GetUserId());
                if (connection != null)
                {
                    if (connection.Dark)
                    {
                        return Content("dark");
                    }
                }
            }
            return Content("light");
        }

        [HttpPost]
        public IActionResult PostStyle()
        {
            var connection = _context.StyleConnections.FirstOrDefault(m => m.UserId == User.GetUserId());
            if (connection == null)
            {
                _context.StyleConnections.Add(new StyleConnections
                {
                    UserId = User.GetUserId(),
                    Dark = true
                });
                _context.SaveChanges();
            }
            else
            {
                connection.Dark = !connection.Dark;
                _context.StyleConnections.Update(connection);
                _context.SaveChanges();
            }
            return Content("ok");
        }
    }
}
