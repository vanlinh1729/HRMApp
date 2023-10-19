using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Attendents;

public class Attendent: FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }
    
    [DataType(DataType.Date)] 
    public DateTime Date { get; set; }
    public Guid EmployeeId { get; set; }
    public int MissingIn { get; set; }
    public int MissingOut { get; set; }
    
    public ICollection<AttendentLine> AttendentLines { get; set; }

    public Attendent(Guid id, Guid? tenantId, DateTime date, Guid employeeId, int missingIn, int missingOut, ICollection<AttendentLine> attendentLines) : base(id)
    {
        TenantId = tenantId;
        Date = date;
        EmployeeId = employeeId;
        MissingIn = missingIn;
        MissingOut = missingOut;
        AttendentLines = attendentLines;
    }

   

    protected Attendent()
    {
    }
}
