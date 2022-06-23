using System.ComponentModel.DataAnnotations;

namespace ProjectWithAspDotNetIdentity.ViewFolder
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
