using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Employees;

public class EmployeeHistory: FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }
    
    public Guid EmployeeId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    
    public string JobPosition { get; set; }
    public string Organization { get; set; }
    public string Description { get; set; }

    public EmployeeHistory(Guid id, Guid? tenantId, Guid employeeId, DateTime start, DateTime end, string jobPosition, string organization, string description) : base(id)
    {
        TenantId = tenantId;
        EmployeeId = employeeId;
        Start = start;
        End = end;
        JobPosition = jobPosition;
        Organization = organization;
        Description = description;
    }

  

    protected EmployeeHistory()
    {
    }
}
