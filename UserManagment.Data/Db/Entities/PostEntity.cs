namespace UserManagment.Data.Db.Entities
{
    public class PostEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }


        public List<CommentEntity> Comments { get; set; }
        public UserEntity User { get; set; }
    }
}
