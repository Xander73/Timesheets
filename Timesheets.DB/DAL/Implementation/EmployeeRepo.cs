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


        public async Task AddItemAsync(Employee item)
        {
            _db.Employees.Add(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var item = _db.Employees.FirstOrDefault(u => u.Id == id);
            item.IsDeleted = true;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _db.Employees.ToListAsync();
        }

        public async Task UpdateItemAsync(Employee item)
        {
            var employee = _db.Employees.FirstOrDefault(u => u.Id == item.Id);
            employee.Position = item.Position;
            employee.IsDeleted = item.IsDeleted;
            employee.UserId = item.UserId;            

            await _db.SaveChangesAsync();
        }
    }
}
