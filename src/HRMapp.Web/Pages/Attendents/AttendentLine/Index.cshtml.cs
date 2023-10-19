using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HRMapp.Attendents;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace HRMapp.Web.Pages.Attendents.AttendentLine;

public class IndexModel : HRMappPageModel
{
    public AttendentLineFilterInput AttendentLineFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class AttendentLineFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "AttendentName")]
    public string AttendentName { get; set; }
    

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "AttendentLineTimeCheck")]
    public DateTime? TimeCheck { get; set; }
    

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "AttendentLineType")]
    public TypeLine? Type { get; set; }
    

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ShiftName")]
    public string ShiftName { get; set; }
}
