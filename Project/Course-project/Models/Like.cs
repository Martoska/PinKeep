using System.ComponentModel.DataAnnotations;

namespace Course_project.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }

        public int ItemId { get; set; }

        public string UserId { get; set; }
    }
}
