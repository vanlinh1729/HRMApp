using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Employees;

public class Employee : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string OtherName { get; set; }
    
    [CanBeNull]
    public Guid? UserId { get; set; }
    
    [CanBeNull]
    public Guid? ContactId { get; set; }
    
    [CanBeNull]
    public Guid? DepartmentId { get; set; }
    
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
    }



    protected Employee()
    {
    }
}
