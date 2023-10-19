using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Employees.EmployeeHistory.ViewModels;

namespace HRMapp.Web.Pages.Employees.EmployeeHistory;

public class CreateModalModel : HRMappPageModel
{
    [BindProperty]
    public CreateEditEmployeeHistoryViewModel ViewModel { get; set; }

    private readonly IEmployeeHistoryAppService _service;

    public CreateModalModel(IEmployeeHistoryAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditEmployeeHistoryViewModel, CreateUpdateEmployeeHistoryDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}