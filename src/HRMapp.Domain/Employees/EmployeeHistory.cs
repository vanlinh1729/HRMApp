using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Employees;

public class EmployeeHistory: FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Organization { get; set; }
    public string Description { get; set; }

    public EmployeeHistory(Guid id, Guid? tenantId, DateTime start, DateTime end, string organization, string description) : base(id)
    {
        TenantId = tenantId;
        Start = start;
        End = end;
        Organization = organization;
        Description = description;
    }

  

    protected EmployeeHistory()
    {
    }
}
