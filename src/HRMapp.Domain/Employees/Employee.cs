using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
