using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Contracts;

public class Contract:Entity<Guid>,IMultiTenant
{
    public Guid? TenantId { get; set; }
    
    public Guid? EmployeeId { get; set; }

    public  TimeContract TimeContract { get; set; }
    
    public  DateTime  SignDate { get; set; }

    public  decimal  CoefficientSalary { get; set; }

    public Contract(Guid id, Guid? tenantId, Guid? employeeId, TimeContract timeContract, DateTime signDate, decimal coefficientSalary) : base(id)
    {
        TenantId = tenantId;
        EmployeeId = employeeId;
        TimeContract = timeContract;
        SignDate = signDate;
        CoefficientSalary = coefficientSalary;
    }

    

    protected Contract()
    {
    }
}
