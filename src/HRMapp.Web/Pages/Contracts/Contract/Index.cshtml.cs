using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HRMapp.Contracts;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace HRMapp.Web.Pages.Contracts.Contract;

public class IndexModel : HRMappPageModel
{
    public ContractFilterInput ContractFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class ContractFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ContractEmployeeId")]
    public string? EmployeeName { get; set; }
    
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ContractEmployeeId")]
    public Guid? EmployeeId { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ContractTimeContract")]
    public TimeContract? TimeContract { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ContractSignDate")]
    public DateTime? SignDate { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ContractCoefficientSalary")]
    public decimal? CoefficientSalary { get; set; }
}
