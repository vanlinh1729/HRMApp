using System;
using JetBrains.Annotations;

namespace HRMapp.Employees.Dtos;

public class EmployeeInputUpdateOneFieldDto
{
    public Guid Id { get; set; }
    
    [CanBeNull]
    public Guid? DepartmentId { get; set; }
}