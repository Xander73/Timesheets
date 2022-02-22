

namespace Core.Models.Entities
{
    public class Employee : BaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Position { get; set; }        
    }
}
