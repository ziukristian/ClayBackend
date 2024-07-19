using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClayBackend.Entities
{
    public class User: IdentityUser<Guid>
    {
        public IList<GroupMembership> GroupMemberships { get; set; } = [];
        public IList<Group> Groups { get; set; } = [];
        public IList<UserPermission> Permissions { get; set; } = [];
        public IList<ActivityLog> ActivityLogs { get; set; } = [];
    }
}
