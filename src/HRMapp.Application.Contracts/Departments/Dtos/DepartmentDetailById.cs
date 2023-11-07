using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Departments.Dtos;

public class DepartmentDetailById : PagedAndSortedResultRequestDto
{
    public Guid? Id { get; set; }
    public string? EmployeeName { get; set; } 
    public int MaxResultCount { get; set; } = (int)999999999;

}