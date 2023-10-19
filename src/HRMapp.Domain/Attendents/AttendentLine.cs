using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Attendents;

public class AttendentLine: FullAuditedAggregateRoot<Guid>,IMultiTenant
{
  

    public Guid? TenantId { get; set; }
    public Guid AttendentId { get; set; }
    public DateTime TimeCheck { get; set; }
    public TypeLine Type { get; set; }
    public Guid ShiftId { get; set; }
    
    public int TimeMissingIn { get; set; }
    
    public int TimeMissingOut { get; set; }

    public AttendentLine(Guid id, Guid? tenantId, Guid attendentId, DateTime timeCheck, TypeLine type, Guid shiftId, int timeMissingIn, int timeMissingOut) : base(id)
    {
        TenantId = tenantId;
        AttendentId = attendentId;
        TimeCheck = timeCheck;
        Type = type;
        ShiftId = shiftId;
        TimeMissingIn = timeMissingIn;
        TimeMissingOut = timeMissingOut;
    }

 

    protected AttendentLine()
    {
    }
}
