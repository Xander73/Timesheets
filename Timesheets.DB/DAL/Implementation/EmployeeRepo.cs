using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Timesheets.DB.DAL.Context;
using Timesheets.DB.DAL.Interfaces;

namespace Timesheets.DB.DAL.Implementation
{
    public class EmployeeRepo : IEmployeeRepo
    {
        MyDbContext _db;


        public EmployeeRepo(MyDbContext db)
        {
            _db = db;
        }


        public async Task AddItemAsync(Employee item, CancellationToken token)
        {
<<<<<<< HEAD
            _db.Employees.Add(item);
            await _db.SaveChangesAsync();
=======
            _db.Employees.AddAsync(item);
            var addedEmployeeId = await _db.Employees.LastAsync();
            await _db.SaveChangesAsync();

            return addedEmployeeId.Id;
>>>>>>> f379f9d (Add authorization and authotication)
        }

        public async Task DeleteItemAsync(Guid id, CancellationToken token)
        {
<<<<<<< HEAD
            var item = _db.Employees.FirstOrDefault(u => u.Id == id);
            item.IsDeleted = true;
            await _db.SaveChangesAsync();
=======
            Employee employee = await _db.Employees.FirstOrDefaultAsync(u => u.Id == id);
            if (employee != null)
            {
                employee.IsDeleted = true;
                await _db.SaveChangesAsync();
            }
>>>>>>> f379f9d (Add authorization and authotication)
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken token)
        {
            return await _db.Employees.ToListAsync();
        }

        public async Task UpdateItemAsync(Employee item, CancellationToken token)
        {
            var employee = _db.Employees.FirstOrDefault(u => u.Id == item.Id);
            employee.Position = item.Position;
            employee.IsDeleted = item.IsDeleted;
            employee.UserId = item.UserId;            

            await _db.SaveChangesAsync();
        }
    }
}
