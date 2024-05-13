using Microsoft.AspNetCore.Identity;

namespace Ecom.Services.AuthApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
