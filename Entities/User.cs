using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClayBackend.Entities
{
    public class User: IdentityUser<Guid>
    {
        public IEnumerable<GroupMember> GroupMemberships { get; set; } = [];
        public IEnumerable<UserPermission> Permissions { get; set; } = [];
    }
}
