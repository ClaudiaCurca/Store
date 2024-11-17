using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class User
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; } = "";
        [Display(Name = "Email")]
        public string Email { get; set; } = "";
    }
}
