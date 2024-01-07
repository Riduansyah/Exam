using Domain.Common;

namespace Domain.Entities
{
    public partial class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        //public string PasswordHash { get; set; }
        //public string PasswordSalt { get; set; }
    }
}
