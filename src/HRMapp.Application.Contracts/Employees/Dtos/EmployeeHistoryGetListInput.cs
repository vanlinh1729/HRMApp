using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Employees.Dtos;

[Serializable]
public class EmployeeHistoryGetListInput : PagedAndSortedResultRequestDto
{
    public string? EmployeeName { get; set; }
    
    public Guid? EmployeeId { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }
    public string? Datetime { get; set; }

    public string? JobPosition { get; set; }
    public string? Organization { get; set; }

    public string? Description { get; set; }
    
    public int MaxResultCount { get; set; } = (int)999999999;

}