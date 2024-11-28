using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username or Email is required")]
        [DisplayName("Username or Email")]
        public string UserNameOrEmail { get; set; }
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
