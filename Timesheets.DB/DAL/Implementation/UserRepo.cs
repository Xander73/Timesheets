using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Timesheets.DB.DAL.Context;
using Timesheets.DB.DAL.Interfaces;

namespace Timesheets.DB.DAL.Implementation
{
    public class UserRepo : IUserRepo
    {
        MyDbContext _db;


        public UserRepo(MyDbContext db)
        {
            _db = db;
        }

        public async Task AddItemAsync(User item)
        {
            _db.Users.Add(item);
            await _db.SaveChangesAsync();
        }


        public async Task DeleteItemAsync(Guid id)
        {
            var item = _db.Users.FirstOrDefault(u => u.Id == id);
            item.IsDeleted = true;
            await _db.SaveChangesAsync();
        }


        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }


        public async Task UpdateItemAsync(User item)
        {
            var itemDb = _db.Users.FirstOrDefault(u => u.Id == item.Id);
            itemDb.Comment = item.Comment;
            itemDb.LastName = item.LastName;
            itemDb.FirstName = item.FirstName;
            itemDb.MiddleName = item.MiddleName;
            itemDb.IsDeleted = item.IsDeleted;

            await _db.SaveChangesAsync();
        }
    }
}
