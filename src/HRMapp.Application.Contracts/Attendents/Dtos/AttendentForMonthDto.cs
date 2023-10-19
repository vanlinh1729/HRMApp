using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Attendents.Dtos;

[Serializable]
public class AttendentForMonthDto : FullAuditedEntityDto<Guid>
{
    public Guid EmployeeId { get; set; }

    public DateTime Month { get; set; }
}