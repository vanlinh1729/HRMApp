using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HRMapp.Employees;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Departments;

public class Department: FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    [DisableAuditing]
    public Guid? TenantId { get; set; }
    
    [DisableAuditing]
    public string Name { get; set; }
    
    public Guid? OwnerId { get; set; }
    
    [DisableAuditing]
    public Guid? ParentId { get; set; }
    
    public ICollection<Employee> Employees { get; set; }

    public Department(Guid id, Guid? tenantId, string name, Guid? ownerId, Guid? parentId) : base(id)
    {
        TenantId = tenantId;
        Name = name;
        OwnerId = ownerId;
        ParentId = parentId;
        Employees = new Collection<Employee>();
    }


    protected Department()
    {
    }
}
