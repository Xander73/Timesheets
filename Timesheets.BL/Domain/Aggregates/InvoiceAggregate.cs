using Core.Models.Entities;
using System.Threading;
using Timesheets.DB.DAL.Interfaces;

namespace Timesheets.BL.Domain.Aggregates
{
    public class InvoiceAggregate : Invoice
    {
        IInvoiceRepo _invoiceRepo;


        public InvoiceAggregate(IInvoiceRepo invoiceRepo)
        {
            _invoiceRepo = invoiceRepo;
        }


        public static Invoice Create ()
        {
            return new Invoice ()
            {
                Id = Guid.NewGuid (),
                Date = DateTime.Now
            };
        }


        public void AddSheet (Sheet sheet)
        {
            Sheets.Add (sheet);
        }


        public  Guid SaveInvoice(CancellationToken token)
        {
            return  _invoiceRepo.AddItem(this, token).Result;
        }
    }
}
