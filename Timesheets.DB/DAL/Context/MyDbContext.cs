using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Timesheets.DB.DAL.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
