

namespace Core.Models.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string Comment { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string RefreshToken { get; set; }
        public string Password { get; set; }
        public long TimeExpires { get; set; }
    }
}
