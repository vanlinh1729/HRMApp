using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClosedXML.Excel;
using HRMapp.Employees;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Identity;

namespace HRMapp.Web.Pages.Employees.Employee;

public class IndexModel : HRMappPageModel
{
    public EmployeeFilterInput EmployeeFilter { get; set; }

    private readonly IEmployeeAppService _service;
    public IndexModel(IEmployeeAppService service)
    {
        _service = service;
    }
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
    public async Task<IActionResult> OnPostImportAsync(IFormFile excel)
    {
        if (excel == null || excel.Length == 0)
        {
            return BadRequest("File is missing.");
        }

        try
        {
            await _service.ImportEmployeeFromExcelAsync(excel);

            return Page();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
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
    
     [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "EmployeePosition")]
    public EmployeePosition? EmployeePosition { get; set; }
    
    
}
