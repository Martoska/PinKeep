using System.ComponentModel.DataAnnotations;

namespace Course_project.Models
{
    public class TagConnection
    {
        [Key]
        public int Id { get; set; }

        public string TagId { get; set; }

        public int ItemId { get; set; }
    }
}
