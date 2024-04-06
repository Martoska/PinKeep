using System.ComponentModel.DataAnnotations;

namespace Course_project.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public int CollectionId { get; set; }

        public string AuthorId { get; set; }

        public string Name { get; set; }

        public string? ImagePath { get; set; }


        public int IntCustom1 { get; set; }

        public int IntCustom2 { get; set; }

        public int IntCustom3 { get; set; }


        public string? StringCustom1 { get; set; }

        public string? StringCustom2 { get; set; }

        public string? StringCustom3 { get; set; }


        public DateTime DateCustom1 { get; set; }

        public DateTime DateCustom2 { get; set; }

        public DateTime DateCustom3 { get; set; }


        public bool BoolCustom1 { get; set; }

        public bool BoolCustom2 { get; set; }

        public bool BoolCustom3 { get; set; }


        public string? LargeDescriptionCustom1 { get; set; }

        public string? LargeDescriptionCustom2 { get; set; }

        public string? LargeDescriptionCustom3 { get; set; }
    }
}
