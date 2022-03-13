using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Timesheets.DB.DAL.Context;
using Timesheets.DB.DAL.Interfaces;

namespace Timesheets.DB.DAL.Implementation
{
    public class InvoiceRepo : IInvoiceRepo
    {
        MyDbContext _db;


        public InvoiceRepo(MyDbContext db)
        {
            _db = db;
        }


        public async Task<Guid> AddItem(Invoice item, CancellationToken token)
        {
            _db.Invoices.AddAsync(item);
            await _db.SaveChangesAsync();
            var addedSheetId = await _db.Invoices.LastAsync();

            return addedSheetId.Id;
        }


        public async Task DeleteItem(Guid id, CancellationToken token)
        {
            Invoice invoice = await _db.Invoices.FirstOrDefaultAsync(u => u.Id == id);
            if (invoice != null)
            {
                invoice.IsDeleted = true;
                await _db.SaveChangesAsync();
            }
        }


        public async Task<List<Invoice>> GetAll(CancellationToken token) => await _db.Invoices.Include(i => i.Sheets).ToListAsync();


        public async Task<Invoice> Get(Guid id, CancellationToken token)
        {
            return await _db.Invoices.Include(i => i.Sheets).FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<IEnumerable<Invoice>> GetSomeItems(int skip, int take, CancellationToken token)
        {
            int sheetCount = await _db.Sheets.CountAsync();
            IEnumerable<Invoice> invoices;
            if (skip < sheetCount)
            {
                invoices = _db.Invoices.Include(i => i.Sheets).Skip(skip).Take(take);

                return invoices;
            }
            else
            {
                invoices = _db.Invoices.Include(i => i.Sheets).Skip(sheetCount - take).Take(take);
                return invoices;
            }
        }


        public async Task<Invoice> UpdateItem(Invoice item, CancellationToken token)
        {
            Invoice invoice = await _db.Invoices.Include(i => i.Sheets).FirstOrDefaultAsync(i => i.Id == item.Id);
            if (invoice != null)
            {
                invoice.IsDeleted = item.IsDeleted;
                invoice.Date = item.Date;
                invoice.Sheets = item.Sheets;
                invoice.IsDeleted = item.IsDeleted;

                await _db.SaveChangesAsync();
            }
            return invoice;
        }

        public Task<Invoice> GetByTerm([MinLength(3), StringLength(50)] string term, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
