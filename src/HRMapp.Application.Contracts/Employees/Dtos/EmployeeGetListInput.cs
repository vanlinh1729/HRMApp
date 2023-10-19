using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Employees.Dtos;

[Serializable]
public class EmployeeGetListInput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }


    public string? OtherName { get; set; }


    public string? UserName { get; set; }
    public Guid? UserId { get; set; }


    public string? ContactName { get; set; }
    public Guid? ContactId { get; set; }


    public string? DepartmentName { get; set; }
    public Guid? DepartmentId { get; set; }


    public StatusEmployee? Status { get; set; }

    
}