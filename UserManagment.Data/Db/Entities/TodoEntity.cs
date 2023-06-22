namespace UserManagment.Data.Db.Entities
{
    public class TodoEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }

        public UserEntity User { get; set; }
    }
}
