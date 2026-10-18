using EmployeeApi.Models;
using EmployeeApi.Data;
using Microsoft.EntityFrameworkCore;
namespace EmployeeApi.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly AppDbContext _db;
        public EmployeeRepository(AppDbContext db) { 
            _db = db;
        }
        public async Task<(List<Employee> Items, int Total)> GetPagedAsync(int page, int pageSize, string? keyword)
        {
            var query = _db.Employees.Include(e => e.Department).AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(e => e.Name.Contains(keyword));
            }
            int total = await query.CountAsync();
            var items = await query.OrderBy(e => e.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _db.Employees.Include(e => e.Department).AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<bool> DepartmentExistsAsync(int departmentId)
        {
            return await _db.Departments.AnyAsync(d => d.Id == departmentId);
        }
        public async Task<bool> NameExistsAsync(string name)
        {
            return await _db.Employees.AnyAsync(e => e.Name == name);
        }
        public async Task<Employee> AddAsync(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            return employee;
        }
        public async Task UpdateAsync(Employee employee)
        {
            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(Employee employee)
        {
            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
        }
    }
}
