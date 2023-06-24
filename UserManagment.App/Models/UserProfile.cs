using System.ComponentModel.DataAnnotations;
using UserManagment.Data.Db.Entities;

namespace UserManagment.App.Models
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
    }
}
