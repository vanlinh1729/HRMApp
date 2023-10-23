using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Employees.Employee.ViewModels;
using HRMapp.Web.Pages.Employees.EmployeeHistory.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMapp.Web.Pages.Employees.EmployeeHistory;

public class CreateOrEditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    public List<SelectListItem> Employees { get; set; }
   
    [BindProperty]
    public CreateEditEmployeeHistoryViewModel ViewModel { get; set; }

    private readonly IEmployeeHistoryAppService _service;

    public CreateOrEditModalModel(IEmployeeHistoryAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        if (!IsCreate())
        {
            var dto = await _service.GetEmployeeHistoryDetail(Id);
            ViewModel = ObjectMapper.Map<EmployeeHistoryDto, CreateEditEmployeeHistoryViewModel>(dto);
        }
        else
        {
            ViewModel = new CreateEditEmployeeHistoryViewModel();
        }
        var employees = await _service.GetListEmployees();
        Employees = employees.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        if (!IsCreate())
        {
            var dto = ObjectMapper.Map<CreateEditEmployeeHistoryViewModel, CreateUpdateEmployeeHistoryDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
        }
        else
        {
            var dto = ObjectMapper.Map<CreateEditEmployeeHistoryViewModel, CreateUpdateEmployeeHistoryDto>(ViewModel);
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