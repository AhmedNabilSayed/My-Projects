using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models
{
	public class DepartmentViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Cod Is Required")]
		public string Code { get; set; }
		[Required(ErrorMessage = "Name Is Required")]
		[MaxLength(50)]
		public string Name { get; set; }
		public DateTime DateOfCreation { get; set; }
	}
}
