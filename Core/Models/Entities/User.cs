

using System.ComponentModel.DataAnnotations;

namespace Core.Models.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string Comment { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string RefreshToken { get; set; }
        [Required]
        public string Password { get; set; }
        public long TimeExpires { get; set; }
    }
}
