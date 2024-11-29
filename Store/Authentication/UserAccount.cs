using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Store.Authentication
{
	[Index(nameof(Email), IsUnique = true)]
	[Index(nameof(UserName), IsUnique = true)]
	public class UserAccount
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
		[Required]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string UserName { get; set; } = string.Empty;
	}
}
