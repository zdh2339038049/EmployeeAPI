using EmployeeApi.Common;
using EmployeeApi.DTOs;
using EmployeeApi.Models;
using EmployeeApi.Repositories;

namespace EmployeeApi.Services;

public class EmployeeService : IEmployeeService    // ← 注意这里
{
    private readonly IEmployeeRepository _repo;
    public EmployeeService(IEmployeeRepository repo) => _repo = repo;

    public async Task<Result<PagedResult<EmployeeDto>>> GetPagedAsync(int page, int pageSize, string? keyword)
    {
        var (items, total) = await _repo.GetPagedAsync(page, pageSize, keyword);
        var dtos = items.Select(e => new EmployeeDto
        {
            Id = e.Id,
            Name = e.Name,
            Salary = e.Salary,
            DepartmentId = e.DepartmentId,
            DepartmentName = e.Department?.Name ?? "未知部门"
        }).ToList();
        return Result.Ok(new PagedResult<EmployeeDto>
        {
            Items = dtos,
            Total = total,
            Page = page,
            PageSize = pageSize
        });
    }

    public async Task<Result<EmployeeDto>> GetByIdAsync(int id)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e == null) return Result.Fail<EmployeeDto>(404, $"找不到 Id 为 {id} 的员工");
        return Result.Ok(new EmployeeDto
        {
            Id = e.Id,
            Name = e.Name,
            Salary = e.Salary,
            DepartmentId = e.DepartmentId,
            DepartmentName = e.Department?.Name ?? "未知部门"
        });
    }

    public async Task<Result<int>> CreateAsync(EmployeeCreateDto dto)
    {
        if (!await _repo.DepartmentExistsAsync(dto.DepartmentId))
            return Result.Fail<int>(400, "所选部门不存在");

        if (await _repo.NameExistsAsync(dto.Name))
            return Result.Fail<int>(400, $"已存在名为 {dto.Name} 的员工");
        var employee = new Employee
        {
            Name = dto.Name,
            Salary = dto.Salary,
            DepartmentId = dto.DepartmentId
        };
        await _repo.AddAsync(employee);
        return Result.Ok(employee.Id, "新增成功");
    }

    public async Task<Result> UpdateAsync(int id, EmployeeUpdateDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return Result.Fail(404, $"找不到 Id 为 {id} 的员工");
        if (!await _repo.DepartmentExistsAsync(dto.DepartmentId))
            return Result.Fail(400, "所选部门不存在");
        existing.Name = dto.Name;
        existing.Salary = dto.Salary;
        existing.DepartmentId = dto.DepartmentId;
        await _repo.UpdateAsync(existing);
        return Result.Ok("修改成功");
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return Result.Fail(404, $"找不到 Id 为 {id} 的员工");
        await _repo.DeleteAsync(existing);
        return Result.Ok("删除成功");
    }
}