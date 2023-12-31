using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Attendents;

public class AttendentForMonth: FullAuditedAggregateRoot<Guid>,IMultiTenant
{
  

    public Guid? TenantId { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime Month { get; set; }
    
    public float Count { get; set; }

    public AttendentForMonth(Guid id, Guid? tenantId, Guid employeeId, DateTime month, float count) : base(id)
    {
        TenantId = tenantId;
        EmployeeId = employeeId;
        Month = month;
        Count = count;
    }

 

    protected AttendentForMonth()
    {
    }
}
