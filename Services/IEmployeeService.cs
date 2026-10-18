using EmployeeApi.Common;
using EmployeeApi.DTOs;

namespace EmployeeApi.Services;

public interface IEmployeeService
{
    Task<Result<PagedResult<EmployeeDto>>> GetPagedAsync(int page, int pageSize, string? keyword);
    Task<Result<EmployeeDto>> GetByIdAsync(int id);
    Task<Result<int>> CreateAsync(EmployeeCreateDto dto);
    Task<Result> UpdateAsync(int id, EmployeeUpdateDto dto);
    Task<Result> DeleteAsync(int id);
}
