using Microsoft.AspNetCore.Identity;

namespace ClayBackend.Models
{
    public class Role : IdentityRole
    {
        public int AccessLevel { get; set; } = 0;
    }
}
