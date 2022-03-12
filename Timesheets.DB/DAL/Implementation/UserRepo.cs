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


        public async Task<Guid> AddItem(User item, CancellationToken token)
        {
            _db.Users.AddAsync(item);
            var addedUserId = await _db.Users.LastAsync();

            return addedUserId.Id;
        }


        public async Task DeleteItem(Guid id, CancellationToken token)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                user.IsDeleted = true;
            }
        }


        public async Task<IEnumerable<User>> GetAll(CancellationToken token) => await _db.Users.ToListAsync();


        public async Task<User> Get(Guid id, CancellationToken token)
        {
            return await _db.Users.FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<IEnumerable<User>> GetSomePersons(int skip, int take, CancellationToken token)
        {
            int userCount = await _db.Users.CountAsync();
            IEnumerable<User> users;
            if (skip < userCount)
            {
                users = _db.Users.Skip(skip).Take(take);

                return users;
            }
            else
            {
                users = _db.Users.Skip(userCount - take).Take(take);
                return users;
            }
        }


        public async Task<User> GetByTerm(string term, CancellationToken token)
        {
            return await _db.Users.FirstOrDefaultAsync(i => i.FirstName == term);
        }


        public async Task<User> UpdateItem(User item, CancellationToken token)
        {
            User user = await _db.Users.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (user != null)
            {
                var itemDb = _db.Users.FirstOrDefault(u => u.Id == item.Id);
                itemDb.Comment = item.Comment;
                itemDb.LastName = item.LastName;
                itemDb.FirstName = item.FirstName;
                itemDb.MiddleName = item.MiddleName;
                itemDb.IsDeleted = item.IsDeleted;

                await _db.SaveChangesAsync();
            }
            return user;
        }
    }
}
