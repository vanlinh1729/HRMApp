using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Contracts.Dtos;

[Serializable]
public class ContractDto : EntityDto<Guid>
{
    public Guid? EmployeeId { get; set; }

    public TimeContract TimeContract { get; set; }

    public DateTime SignDate { get; set; }

    public decimal CoefficientSalary { get; set; }
}