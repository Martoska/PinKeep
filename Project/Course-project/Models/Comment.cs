using System.ComponentModel.DataAnnotations;

namespace Course_project.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public int ItemId { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public DateTime DateTime { get; set; }

        public string Message { get; set; }
    }
}
