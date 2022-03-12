using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entities
{
    public class Sheet : BaseEntity<Guid>
	{
		public DateTime ApproveDate { get; protected set; }
		public bool IsApproved { get; protected set; }
		public int Amount { get; set; }
		public Guid InvoiceId { get; set; }

		public void Approve()
		{
			IsApproved = true;
			ApproveDate = DateTime.Now;
		}
	}
}
