using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Salarys.Dtos;

[Serializable]
public class SalaryDto : FullAuditedEntityDto<Guid>
{
    
    public Guid? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }

    public Guid? AttendentForMonthId { get; set; }
    public DateTime? AttendentForMonthMonth { get; set; }
    public decimal? TotalSalary { get; set; }
}