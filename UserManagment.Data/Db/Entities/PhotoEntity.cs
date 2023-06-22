namespace UserManagment.Data.Db.Entities
{
    public class PhotoEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }

        public UserEntity User { get; set; }
    }
}
