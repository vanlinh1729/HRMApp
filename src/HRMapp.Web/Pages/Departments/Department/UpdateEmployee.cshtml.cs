using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Departments;
using HRMapp.Departments.Dtos;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Departments.Department.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMapp.Web.Pages.Departments.Department;

public class UpdateEmployee : HRMappPageModel
{

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    public List<SelectListItem> Owners { get; set; }
    public List<SelectListItem> Parents { get; set; }
    [BindProperty]
    public CreateEditDepartmentViewModel ViewModel { get; set; }
    public List<EmployeeNameViewModel> ViewEmployeeNameDepartmentModels { get; set; }

    private readonly IDepartmentAppService _service;

    public UpdateEmployee(IDepartmentAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetDepartmentDetail(Id);
        ViewModel = ObjectMapper.Map<DepartmentDto, CreateEditDepartmentViewModel>(dto);
        
        var owners = await _service.GetListOwnerAsync();
        Owners = owners.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
        var parents = await _service.GetListParentAsync();
        Parents = parents.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
        var listemployeenamedepartment = await _service.GetListEmployeeNameDepartment(Id);
        ViewEmployeeNameDepartmentModels = ObjectMapper.Map<List<EmployeeWithName>,List<EmployeeNameViewModel>>(listemployeenamedepartment);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditDepartmentViewModel, CreateDepartmentAndAddEmployee>(ViewModel);
        await _service.UpdateDepartmentWithManyEmployeeAsync(Id, dto);
        return NoContent();
    }
    
    bool IsCreate()
    {
        if (Id != Guid.Empty)
        {
            return false;
        }
        return true;
    }

}

