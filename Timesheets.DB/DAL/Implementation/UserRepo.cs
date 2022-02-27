using Core.Models.Entities;
using Timesheets.DB.DAL.Context;
using Timesheets.DB.DAL.Interfaces;

namespace Timesheets.DB.DAL.Implementation
{
    public class UserRepo : IUserRepo
    {
        private MyDbContext _db;


        public UserRepo(MyDbContext db)
        {
            _db = db;
        }


        public Guid AddItem(User item)
        {
<<<<<<< HEAD
            _db.Users.Add(item);
=======
            _db.Users.AddAsync(item);
            await _db.SaveChangesAsync();
            var addedUserId = await _db.Users.LastAsync();
>>>>>>> f379f9d (Add authorization and authotication)

            return _db.Users.Last().Id;
        }


        public void DeleteItem(Guid id)
        {
            User user = Get(id);
            if (user != null)
            {
                user.IsDeleted = true;
                await _db.SaveChangesAsync();
            }
        }


        public List<User> GetAll() => _db.Users.ToList();


        public User Get(Guid id)
        {
            return _db.Users.FirstOrDefault(i => i.Id == id);
        }


        public IEnumerable<User> GetSomePersons(int skip, int take)
        {
            int usersCount = _db.Users.ToList().Count;
            IEnumerable<User> users;
            if (skip < usersCount)
            {
                users = _db.Users.Skip(skip).Take(take);

                return users;
            }
            else
            {
                users = _db.Users.Skip(usersCount - take).Take(take);
                return users;
            }
        }


        public User GetByTerm(string term)
        {
            return _db.Users.FirstOrDefault(i => i.FirstName == term);
        }


        public User UpdateItem(User item)
        {
            User user = _db.Users.FirstOrDefault(i => i.Id == item.Id);
            if (user != null)
            {
<<<<<<< HEAD
                user.Comment = item.Comment;
                user.LastName = item.LastName;
                user.FirstName = item.FirstName;
                user.MiddleName = item.MiddleName;
                user.IsDeleted = item.IsDeleted;
=======
                var itemDb = _db.Users.FirstOrDefault(u => u.Id == item.Id);
                itemDb.Comment = item.Comment;
                itemDb.LastName = item.LastName;
                itemDb.FirstName = item.FirstName;
                itemDb.MiddleName = item.MiddleName;
                itemDb.IsDeleted = item.IsDeleted;
                itemDb.Password = item.Password;
                itemDb.RefreshToken = item.RefreshToken;

                await _db.SaveChangesAsync();
>>>>>>> f379f9d (Add authorization and authotication)
            }
            return user;
        }
    }
}
