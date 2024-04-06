using System.ComponentModel.DataAnnotations;

namespace Course_project.Models
{
    public class Tag
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}



