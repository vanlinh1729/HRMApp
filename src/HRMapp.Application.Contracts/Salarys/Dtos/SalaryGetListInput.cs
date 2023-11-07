using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Salarys.Dtos;

[Serializable]
public class SalaryGetListInput : PagedAndSortedResultRequestDto
{
    public Guid? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }

    public Guid? AttendentForMonthId { get; set; }
    public DateTime? AttendentForMonthMonth { get; set; }
    public decimal? TotalSalary { get; set; }
    
    public int MaxResultCount { get; set; } = (int)999999999;

}