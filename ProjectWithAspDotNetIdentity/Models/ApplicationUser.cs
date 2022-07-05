using Microsoft.AspNetCore.Identity;

namespace ProjectWithAspDotNetIdentity.Models
{
    public class ApplicationUser:IdentityUser
    {
        public String Gender { get; set; }
    }
}
