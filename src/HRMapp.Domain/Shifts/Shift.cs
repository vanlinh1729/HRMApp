using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Shifts;

public class Shift:Entity<Guid>,IMultiTenant
{
    public Guid? TenantId { get; set; }

    public  string Name { get; set; }
    
    public  TimeSpan  Start { get; set; }
    
    public  TimeSpan End { get; set; }
    
    public  TimeSpan  TimeStartCheckin { get; set; }

    public  TimeSpan  TimeStopCheckout { get; set; }

    public Shift(Guid id, Guid? tenantId, string name, TimeSpan start, TimeSpan end, TimeSpan timeStartCheckin, TimeSpan timeStopCheckout) : base(id)
    {
        TenantId = tenantId;
        Name = name;
        Start = start;
        End = end;
        TimeStartCheckin = timeStartCheckin;
        TimeStopCheckout = timeStopCheckout;
    }



    protected Shift()
    {
    }
}
