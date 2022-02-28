

using System.ComponentModel.DataAnnotations;

namespace Core.Models.Entities
{
    public class Employee : BaseEntity<Guid>
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Position { get; set; }        
    }
}
