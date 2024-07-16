using Microsoft.AspNetCore.Identity;

namespace ClayBackend.Models
{
    public class User: IdentityUser
    {
        public int AccessLevel { get; set; } = 0;
    }
}
