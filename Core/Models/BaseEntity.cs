using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class BaseEntity<TUniqueId> where TUniqueId : struct
    {
        public TUniqueId Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
