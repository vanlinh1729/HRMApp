using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Salarys.Dtos;

[Serializable]
public class SalaryDto : FullAuditedEntityDto<Guid>
{
    public int Count { get; set; }
    public string DepartmentName { get; set; }
    public decimal CoefficientSalary { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public float AttendentForMonthCount { get; set; }
    public Guid AttendentForMonthId { get; set; }
    public DateTime AttendentForMonthMonth { get; set; }
    public decimal TotalSalary { get; set; }
}