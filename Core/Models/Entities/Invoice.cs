using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entities
{
    public class Invoice : BaseEntity<Guid>
    {
        public DateTime Date { get; set; }
        public List<Sheet> Sheets { get; set; } = new List<Sheet>();
    }
}
