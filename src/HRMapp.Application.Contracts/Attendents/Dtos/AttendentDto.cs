using System;
using System.Collections.Generic;
using HRMapp.Permissions;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Attendents.Dtos;

[Serializable]
public class AttendentDto : FullAuditedEntityDto<Guid>
{
    public DateTime Date { get; set; }

    public Guid EmployeeId { get; set; }

    public int MissingIn { get; set; }

    public int MissingOut { get; set; }

    public List<AttendentLineDto> AttendentLines { get; set; }
}