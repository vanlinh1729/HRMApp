using System;

namespace HRMapp.Employees.Dtos;

public class EmployeeWithDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DepartmentName { get; set; }
    
}