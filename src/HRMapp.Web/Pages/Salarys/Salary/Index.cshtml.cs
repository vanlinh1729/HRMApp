using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace HRMapp.Web.Pages.Salarys.Salary;

public class IndexModel : HRMappPageModel
{
    public SalaryFilterInput SalaryFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class SalaryFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "SalaryEmployeeName")]
    public string? EmployeeName { get; set; } 
    
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "SalaryEmployeeId")]
    public Guid? EmployeeId { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "SalaryAttendentForMonthId")]
    public Guid? AttendentForMonthId { get; set; }
    
     [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "SalaryAttendentForMonthId")]
    public float? AttendentForMonthCount { get; set; }
    
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "SalaryAttendentForMonthId")]
    public DateTime? AttendentForMonthMonth { get; set; }
}
