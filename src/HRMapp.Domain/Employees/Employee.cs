using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Employees;

[Audited]
public class Employee : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    [DisableAuditing]
    public Guid? TenantId { get; set; }
    
    [DisableAuditing]

    [Required]
    public string Name { get; set; }
    
    [DisableAuditing]

    public string OtherName { get; set; }
    
    [DisableAuditing]

    [CanBeNull]
    public Guid? UserId { get; set; }
    
    [DisableAuditing]

    [CanBeNull]
    public Guid? ContactId { get; set; }
    
    [CanBeNull]
    public Guid? DepartmentId { get; set; }
    [DisableAuditing]

    public ICollection<EmployeeHistory> EmployeeHistories { get; set; }

    
    [DefaultValue("Offline")]
    public StatusEmployee Status { get; set; }

    public Employee(Guid id, Guid? tenantId, string name, string otherName, Guid? userId, Guid? contactId, Guid? departmentId, StatusEmployee status) : base(id)
    {
        TenantId = tenantId;
        Name = name;
        OtherName = otherName;
        UserId = userId;
        ContactId = contactId;
        DepartmentId = departmentId;
        Status = status;
        EmployeeHistories = new Collection<EmployeeHistory>();
    }



    protected Employee()
    {
    }
}
