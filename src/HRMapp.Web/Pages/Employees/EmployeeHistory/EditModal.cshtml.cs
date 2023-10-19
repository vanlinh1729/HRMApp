using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Employees.EmployeeHistory.ViewModels;

namespace HRMapp.Web.Pages.Employees.EmployeeHistory;

public class EditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditEmployeeHistoryViewModel ViewModel { get; set; }

    private readonly IEmployeeHistoryAppService _service;

    public EditModalModel(IEmployeeHistoryAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<EmployeeHistoryDto, CreateEditEmployeeHistoryViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditEmployeeHistoryViewModel, CreateUpdateEmployeeHistoryDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}