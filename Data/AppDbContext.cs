using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;
namespace EmployeeApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Department> Departments => Set<Department>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "IT部" },
                new Department { Id = 2, Name = "人事部" },
                new Department { Id = 3, Name = "财务部" }
            );
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "张三", Salary = 3000, DepartmentId = 1 },
                new Employee { Id = 2, Name = "李四", Salary = 2500, DepartmentId = 2 },
                new Employee { Id = 3, Name = "王五", Salary = 2800, DepartmentId = 2 },
                new Employee { Id = 4, Name = "赵六", Salary = 3200, DepartmentId = 1 }
            );
        }
    }
}
