using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Employees.Dtos;

[Serializable]
public class EmployeeHistoryDto : FullAuditedEntityDto<Guid>
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string Organization { get; set; }

    public string Description { get; set; }
}