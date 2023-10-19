using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Attendents.Dtos;

[Serializable]
public class AttendentLineDto : FullAuditedEntityDto<Guid>
{
    public Guid AttendentId { get; set; }

    public DateTime TimeCheck { get; set; }

    public TypeLine Type { get; set; }

    public Guid ShiftId { get; set; }

    public int TimeMissingIn { get; set; }

    public int TimeMissingOut { get; set; }
}