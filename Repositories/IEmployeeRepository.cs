using EmployeeApi.Models;
namespace EmployeeApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<(List<Employee> Items, int Total)> GetPagedAsync(int page, int pageSize, string? keyword);
        Task<Employee?> GetByIdAsync(int id);
        Task<bool> DepartmentExistsAsync(int departmentId);
        Task<bool> NameExistsAsync(string name);
        Task<Employee> AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
    }
}
