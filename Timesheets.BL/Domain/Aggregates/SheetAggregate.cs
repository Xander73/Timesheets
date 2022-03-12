using Core.Models.Entities;

namespace Timesheets.BL.Domain.Aggregates
{
    public class SheetAggregate : Sheet
    {
        public static Sheet Create(int amount, Guid invoiceId)
        {
            return new Sheet()
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                InvoiceId = invoiceId,
            };
        }


        public void Approve()
        {
            IsApproved = true;
            ApproveDate = DateTime.Now;
        }
    }
}
