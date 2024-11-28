using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
	public class RegistrationViewModel
	{
		[Key]

		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;
		[Required]
		//verify if the email is a valid email
		[RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please Enter Valid Email.")]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string UserName { get; set; }
		[Compare("Password", ErrorMessage ="Please confirm your password")]
		[DataType(DataType.Password)]	
        public string ConfirmPassword { get; set; }
    }
}
