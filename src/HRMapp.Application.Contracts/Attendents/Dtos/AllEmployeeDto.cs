using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Attendents.Dtos;

public class AllEmployeeDto : PagedAndSortedResultRequestDto
{
    public int MaxResultCount { get; set; } = (int)999999999;
    public string? DepartmentName { get; set; }
}