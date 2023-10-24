using System;
using System.ComponentModel.DataAnnotations;
using HRMapp.Contracts;

namespace HRMapp.Web.Pages.Contracts.Contract.ViewModels;

public class CreateEditContractViewModel
{
    [Display(Name = "ContractEmployeeId")]
    public string? EmployeeName { get; set; } 
    
    [Display(Name = "ContractEmployeeId")]
    public Guid? EmployeeId { get; set; }

    [Display(Name = "ContractTimeContract")]
    public TimeContract TimeContract { get; set; }

    [Display(Name = "ContractSignDate")]
    public DateTime SignDate { get; set; }

    [Display(Name = "ContractCoefficientSalary")]
    public decimal CoefficientSalary { get; set; }
}
