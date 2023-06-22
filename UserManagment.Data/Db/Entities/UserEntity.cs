using Microsoft.AspNetCore.Identity;

namespace UserManagment.Data.Db.Entities
{
    public class UserEntity: IdentityUser<int>
    {
        public string? Password { get; set; }
        public bool IsActive { get; set; }

        public UserProfileEntity UserProfile { get; set; }
    }
}
