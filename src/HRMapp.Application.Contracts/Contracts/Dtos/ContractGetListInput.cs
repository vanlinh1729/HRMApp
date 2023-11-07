using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Contracts.Dtos;

[Serializable]
public class ContractGetListInput : PagedAndSortedResultRequestDto
{
    public string? EmployeeName { get; set; }
    
    public Guid? EmployeeId { get; set; }

    public TimeContract? TimeContract { get; set; }

    public DateTime? SignDate { get; set; }

    public decimal? CoefficientSalary { get; set; }
    
    public int MaxResultCount { get; set; } = (int)999999999;

}