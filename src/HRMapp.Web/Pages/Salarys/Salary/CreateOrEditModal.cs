using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Salarys;
using HRMapp.Salarys.Dtos;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Salarys.Salary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMapp.Web.Pages.Salarys.Salary;

public class CreateOrEditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    [BindProperty]
    public CreateEditSalaryViewModel ViewModel { get; set; }

    private readonly ISalaryAppService _service;
    public List<SelectListItem> Employees { get; set; }

    public CreateOrEditModalModel(ISalaryAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        if (!IsCreate())
        {
            var dto = await _service.GetAsync(Id);
            
            ViewModel = ObjectMapper.Map<SalaryDto, CreateEditSalaryViewModel>(dto);
        }
        else
        {
            ViewModel = new CreateEditSalaryViewModel();
        }

        var employees = await _service.GetListEmployeeAsync();
        Employees = employees.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        if (!IsCreate())
        {
            var dto = ObjectMapper.Map<CreateEditSalaryViewModel, CreateUpdateSalaryDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
        }
        else
        {
            var dto = ObjectMapper.Map<CreateEditSalaryViewModel, CreateUpdateSalaryDto>(ViewModel);
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