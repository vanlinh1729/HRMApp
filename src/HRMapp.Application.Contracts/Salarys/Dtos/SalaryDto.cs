using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Salarys.Dtos;

[Serializable]
public class SalaryDto : FullAuditedEntityDto<Guid>
{
    public Guid? EmployeeId { get; set; }

    public Guid? AttendentForMonthId { get; set; }
}