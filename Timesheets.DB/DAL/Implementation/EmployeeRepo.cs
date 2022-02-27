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


        public async Task<Guid> AddItem(Employee item, CancellationToken token)
        {
            _db.Employees.AddAsync(item);
            var addedEmployeeId = await _db.Employees.LastAsync();
            await _db.SaveChangesAsync();

            return addedEmployeeId.Id;
        }


        public async Task DeleteItem(Guid id, CancellationToken token)
        {
            Employee employee = await _db.Employees.FirstOrDefaultAsync(u => u.Id == id);
            if (employee != null)
            {
                employee.IsDeleted = true;
                await _db.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Employee>> GetAll(CancellationToken token) => await _db.Employees.ToListAsync();


        public async Task<Employee> Get(Guid id, CancellationToken token)
        {
            return await _db.Employees.FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<IEnumerable<Employee>> GetSomePersons(int skip, int take, CancellationToken token)
        {
            int employeeCount = await _db.Users.CountAsync();
            IEnumerable<Employee> employees;
            if (skip < employeeCount)
            {
                employees = _db.Employees.Skip(skip).Take(take);

                return employees;
            }
            else
            {
                employees = _db.Employees.Skip(employeeCount - take).Take(take);
                return employees;
            }
        }


        //У меня ощущение, что с поиском Юзера по Id я перемудрил и EF сам может как-то определить что искать. Я ошибаюсь?
        public async Task<Employee> GetByTerm(string term, CancellationToken token)
        {
            return await _db.Employees.FirstOrDefaultAsync(i => i.UserId == _db.Users.FirstOrDefault(u => u.FirstName == term).Id);
        }


        public async Task<Employee> UpdateItem(Employee item, CancellationToken token)
        {
            Employee employee = await _db.Employees.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (employee != null)
            {
                var itemDb = _db.Employees.FirstOrDefault(e => e.Id == item.Id);
                

                await _db.SaveChangesAsync();
            }
            return employee;
        }
    }
}
