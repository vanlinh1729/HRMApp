using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth;

public class IndexModel : HRMappPageModel
{
    public AttendentForMonthFilterInput AttendentForMonthFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class AttendentForMonthFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "AttendentForMonthEmployeeName")]
    public string? EmployeeName { get; set; }
    
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "AttendentForMonthEmployeeId")]
    public Guid? EmployeeId { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "AttendentForMonthMonth")]
    public DateTime? Month { get; set; }
}
