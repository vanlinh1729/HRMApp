using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace HRMapp.Web.Pages.Shifts.Shift;

public class IndexModel : HRMappPageModel
{
    public ShiftFilterInput ShiftFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class ShiftFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ShiftName")]
    public string? Name { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ShiftStart")]
    public TimeSpan? Start { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ShiftEnd")]
    public TimeSpan? End { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ShiftTimeStartCheckin")]
    public TimeSpan? TimeStartCheckin { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ShiftTimeStopCheckout")]
    public TimeSpan? TimeStopCheckout { get; set; }
}
