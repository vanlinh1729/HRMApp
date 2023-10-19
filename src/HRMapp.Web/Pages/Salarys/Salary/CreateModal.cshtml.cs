using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Salarys;
using HRMapp.Salarys.Dtos;
using HRMapp.Web.Pages.Salarys.Salary.ViewModels;

namespace HRMapp.Web.Pages.Salarys.Salary;

public class CreateModalModel : HRMappPageModel
{
    [BindProperty]
    public CreateEditSalaryViewModel ViewModel { get; set; }

    private readonly ISalaryAppService _service;

    public CreateModalModel(ISalaryAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditSalaryViewModel, CreateUpdateSalaryDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}