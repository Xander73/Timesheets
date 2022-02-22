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
            _db.Users.Add(item);

            return _db.Users.Last().Id;
        }


        public void DeleteItem(Guid id)
        {
            User user = Get(id);
            if (user != null)
            {
                user.IsDeleted = true;
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
                user.Comment = item.Comment;
                user.LastName = item.LastName;
                user.FirstName = item.FirstName;
                user.MiddleName = item.MiddleName;
                user.IsDeleted = item.IsDeleted;
            }
            return user;
        }
    }
}
