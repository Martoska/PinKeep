using System.ComponentModel.DataAnnotations;
using Course_project.Models.Enums;

namespace Course_project.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string Name { get; set; }

        public CollectionType Type { get; set; }

        public string Description { get; set; }

        //public bool IntExtraField { get; set; }
        public string? IntName1 { get; set; }
        public string? IntName2 { get; set; }
        public string? IntName3 { get; set; }

        //public bool StringExtraField { get; set; }
        public string? StringName1 { get; set; }
        public string? StringName2 { get; set; }
        public string? StringName3 { get; set; }

        //public bool BoolExtraField { get; set; }
        public string? BoolName1 { get; set; }
        public string? BoolName2 { get; set; }
        public string? BoolName3 { get; set; }

        //public bool DateExtraField { get; set; }
        public string? DateName1 { get; set; }
        public string? DateName2 { get; set; }
        public string? DateName3 { get; set; }

        //public bool LargeStringExtraField { get; set; }
        public string? LargeStringName1 { get; set; }
        public string? LargeStringName2 { get; set; }
        public string? LargeStringName3 { get; set; }

    }
}
