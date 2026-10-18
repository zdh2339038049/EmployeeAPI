using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.DTOs;

public class EmployeeCreateDto
{
    [Required(ErrorMessage = "姓名不能为空")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "姓名长度需在2-50之间")]
    public string Name { get; set; } = string.Empty;

    [Range(0, 1000000, ErrorMessage = "工资必须在0到100万之间")]
    public decimal Salary { get; set; }

    [Required(ErrorMessage = "必须选择部门")]
    public int DepartmentId { get; set; }
}