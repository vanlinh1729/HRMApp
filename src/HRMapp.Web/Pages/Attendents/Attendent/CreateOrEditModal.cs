using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.Attendent.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace HRMapp.Web.Pages.Attendents.Attendent;

public class CreateOrEditModalModel : HRMappPageModel
{
    private readonly IAttendentAppService _service;

    public AllEmployeeInput AllEmployeeFilter { get; set; }

    public CreateOrEditModalModel(IAttendentAppService service)
    {
        _service = service;
    }

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public List<SelectListItem> Employees { get; set; }
    public List<SelectListItem> Departments { get; set; }

    [BindProperty] public CreateEditAttendentViewModel ViewModel { get; set; }
    [BindProperty] public CreateManyAttendentViewModel ViewCreateModel { get; set; }

    public virtual async Task OnGetAsync()
    {
        if (!IsCreate())
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<AttendentDto, CreateEditAttendentViewModel>(dto);
        }
        else
        {
            ViewCreateModel = new CreateManyAttendentViewModel();
        }

        var employees = await _service.GetListEmployeeAsync();
        var departments = await _service.GetListDepartmentAsync();
        Employees = employees.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
        Departments = departments.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        if (!IsCreate())
        {
            var dto = ObjectMapper.Map<CreateEditAttendentViewModel, CreateUpdateAttendentDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
        }
        else
        {
            var dto = ObjectMapper.Map<CreateManyAttendentViewModel, CreateManyAttendentDto>(ViewCreateModel);
            await _service.CreateManyAttendentAsync(dto);
        }

        return NoContent();
    }

    private bool IsCreate()
    {
        if (Id != Guid.Empty)
        {
            return false;
        }

        return true;
    }
}

public class AllEmployeeInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "EmployeeName")]
    public string EmployeeName { get; set; } 
    
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "DepartmentName")]
    public string DepartmentName { get; set; }
}