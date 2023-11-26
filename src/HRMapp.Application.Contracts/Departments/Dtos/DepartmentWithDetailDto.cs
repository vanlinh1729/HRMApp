using System;
using HRMapp.Employees;

namespace HRMapp.Departments.Dtos;

[Serializable]
public class DepartmentWithDetailDto
{
    public Guid Id { get; set; }
    public string EmployeeName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public EmployeePosition EmployeePosition { get; set; }
}