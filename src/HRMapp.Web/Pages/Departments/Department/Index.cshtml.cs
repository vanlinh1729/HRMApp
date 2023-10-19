using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace HRMapp.Web.Pages.Departments.Department;

public class IndexModel : HRMappPageModel
{
    public DepartmentFilterInput DepartmentFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class DepartmentFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "DepartmentName")]
    public string Name { get; set; }
    

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "OwnerName")]
    public string OwnerName { get; set; }
    

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ParentName")]
    public string ParentName { get; set; }
}
