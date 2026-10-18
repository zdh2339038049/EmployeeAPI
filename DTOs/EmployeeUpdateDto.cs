using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.DTOs;

public class EmployeeUpdateDto
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 1000000)]
    public decimal Salary { get; set; }

    public int DepartmentId { get; set; }
}