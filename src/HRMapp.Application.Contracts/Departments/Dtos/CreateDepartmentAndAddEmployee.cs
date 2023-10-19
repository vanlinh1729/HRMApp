using System;
using JetBrains.Annotations;

namespace HRMapp.Departments.Dtos;

public class CreateDepartmentAndAddEmployee: CreateUpdateDepartmentDto
{
    [CanBeNull] public Guid[] employeeId { get; set; }

}