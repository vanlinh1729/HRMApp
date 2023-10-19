using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace HRMapp.Web.Pages.Attendents.Attendent;

public class IndexModel : HRMappPageModel
{
    public AttendentFilterInput AttendentFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class AttendentFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "Date")]
    public string? Date { get; set; }
    

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "EmployeeName")]
    public string EmployeeName { get; set; }
    
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "daterange")]
    public string QueryAttendentDateRange { get; set; }
    
    
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "MissingIn")]
    public int? MissingIn { get; set; }
    

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "MissingOut")]
    public int? MissingOut { get; set; }
    
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "Date")]
    public string? Datetime { get; set; }
}
