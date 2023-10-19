using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Employees.Employee.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMapp.Web.Pages.Employees.Employee;

public class CreateOrEditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    public List<SelectListItem> Users { get; set; }
    public List<SelectListItem> Contacts { get; set; }
    public List<SelectListItem> Departments { get; set; }
    [BindProperty]
    public CreateEditEmployeeViewModel ViewModel { get; set; }

    private readonly IEmployeeAppService _service;

    public CreateOrEditModalModel(IEmployeeAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        if (!IsCreate())
        {
            var dto = await _service.GetEmployeeDetail(Id);
            ViewModel = ObjectMapper.Map<EmployeeDto, CreateEditEmployeeViewModel>(dto);
        }
        else
        {
            ViewModel = new CreateEditEmployeeViewModel();
        }
        var hrmusers = await _service.GetListHrmUserAsync();
        Users = hrmusers.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
        var hrmcontacts = await _service.GetListHrmContactAsync();
        Contacts = hrmcontacts.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
        var departments = await _service.GetListDepartmentAsync();
        Departments = departments.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        if (!IsCreate())
        {
            var dto = ObjectMapper.Map<CreateEditEmployeeViewModel, CreateUpdateEmployeeDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
        }
        else
        {
            var dto = ObjectMapper.Map<CreateEditEmployeeViewModel, CreateUpdateEmployeeDto>(ViewModel);
            await _service.CreateAsync(dto);
        }
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