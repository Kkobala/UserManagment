namespace UserManagment.App.Models.Requests
{
    public class UpdateUserProfileRequest
    {
        public string OldFirstName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public int UserId { get; set; }
    }
}
