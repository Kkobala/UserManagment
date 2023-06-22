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


        public List<PostEntity> Posts { get; internal set; }
        public List<PhotoEntity> Photos { get; internal set; }
        public List<TodoEntity> Todos { get; internal set; }
        public List<CommentEntity> Comments { get; internal set; }
        public List<AlbumEntity> Albums { get; internal set; }
    }
}
