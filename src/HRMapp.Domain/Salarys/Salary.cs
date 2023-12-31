using System;
using System.Text.Json;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Salarys;

public class Salary: FullAuditedAggregateRoot<Guid>,IMultiTenant
{
    public Guid? TenantId { get; private set; }
    
    public Guid EmployeeId { get;  set; }
    
    public Guid AttendentForMonthId { get;  set; }
    
    public decimal TotalSalary { get; set; }

    public Salary(Guid id, Guid? tenantId, Guid employeeId, Guid attendentForMonthId, decimal totalSalary) : base(id)
    {
        TenantId = tenantId;
        EmployeeId = employeeId;
        AttendentForMonthId = attendentForMonthId;
        TotalSalary = totalSalary;
    }



    protected Salary()
    {
    }
}
