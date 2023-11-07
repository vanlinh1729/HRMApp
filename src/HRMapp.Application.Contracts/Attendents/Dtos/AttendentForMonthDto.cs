using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Attendents.Dtos;

[Serializable]
public class AttendentForMonthDto : FullAuditedEntityDto<Guid>
{
    
    public string EmployeeName { get; set; }
    public string DepartmentName { get; set; }
    
    public Guid EmployeeId { get; set; }

    public DateTime Month { get; set; }
    
    public float Count { get; set; }
}