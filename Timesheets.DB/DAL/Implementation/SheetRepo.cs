using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Timesheets.DB.DAL.Context;
using Timesheets.DB.DAL.Interfaces;

namespace Timesheets.DB.DAL.Implementation
{
    public class SheetRepo : ISheetRepo
    {
        MyDbContext _db;


        public SheetRepo(MyDbContext db)
        {
            _db = db;
        }


        public async Task<Guid> AddItem(Sheet item, CancellationToken token)
        {
            _db.Sheets.AddAsync(item);
            await _db.SaveChangesAsync();
            var addedSheetId = await _db.Sheets.LastAsync();

            return addedSheetId.Id;
        }


        public async Task DeleteItem(Guid id, CancellationToken token)
        {
            Sheet sheet = await _db.Sheets.FirstOrDefaultAsync(u => u.Id == id);
            if (sheet != null)
            {
                sheet.IsDeleted = true;
                await _db.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Sheet>> GetAll(CancellationToken token) => await _db.Sheets.ToListAsync();


        public async Task<Sheet> Get(Guid id, CancellationToken token)
        {
            return await _db.Sheets.FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<IEnumerable<Sheet>> GetSomePersons(int skip, int take, CancellationToken token)
        {
            int sheetCount = await _db.Sheets.CountAsync();
            IEnumerable<Sheet> sheets;
            if (skip < sheetCount)
            {
                sheets = _db.Sheets.Skip(skip).Take(take);

                return sheets;
            }
            else
            {
                sheets = _db.Sheets.Skip(sheetCount - take).Take(take);
                return sheets;
            }
        }


        public async Task<Sheet> UpdateItem(Sheet item, CancellationToken token)
        {
            Sheet sheet = await _db.Sheets.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (sheet != null)
            {
                var itemDb = _db.Sheets.FirstOrDefault(u => u.Id == item.Id);
                itemDb.Amount = item.Amount;
                itemDb.InvoiceId = item.InvoiceId;
                if (itemDb.IsApproved)
                {
                    itemDb.Approve();
                }
                itemDb.IsDeleted = item.IsDeleted;

                await _db.SaveChangesAsync();
            }
            return sheet;
        }

        public Task<Sheet> GetByTerm([MinLength(3), StringLength(50)] string term, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
