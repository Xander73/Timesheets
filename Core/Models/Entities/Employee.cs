using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entities
{
    public class Employee : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string Position { get; set; }        
    }
}
