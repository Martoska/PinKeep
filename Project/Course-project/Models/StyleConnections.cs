using System.ComponentModel.DataAnnotations;

namespace Course_project.Models
{
	public class StyleConnections
	{
		[Key]
		public int Id { get; set; }

		public string UserId { get; set; }

		public bool Dark { get; set; }
	}
}
