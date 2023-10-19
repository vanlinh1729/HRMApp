using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HRMapp.Employees;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace HRMapp.Web.Pages.Employees.Employee;

public class IndexModel : HRMappPageModel
{
    public EmployeeFilterInput EmployeeFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class EmployeeFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "EmployeeName")]
    public string Name { get; set; }
    

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "EmployeeOtherName")]
    public string OtherName { get; set; }
    

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "HrmUserName")]
    public string UserName { get; set; }
    

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "HrmContactName")]
    public string ContactName { get; set; }
    

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "DepartmentName")]
    public string DepartmentName { get; set; }
    

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "EmployeeStatus")]
    public StatusEmployee? Status { get; set; }
    
    
}
