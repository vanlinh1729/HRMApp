using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace HRMapp.Web.Pages.Employees.EmployeeHistory;

public class IndexModel : HRMappPageModel
{
    public EmployeeHistoryFilterInput EmployeeHistoryFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class EmployeeHistoryFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "EmployeeHistoryEmployeeName")]
    public string? EmployeeName { get; set; }
    
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "EmployeeHistoryStart")]
    public DateTime? Start { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "EmployeeHistoryEnd")]
    public DateTime? End { get; set; } 
    
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "Date")]
    public string? Datetime { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "EmployeeHistoryOrganization")]
    public string? Organization { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "EmployeeHistoryDescription")]
    public string? Description { get; set; }
}
