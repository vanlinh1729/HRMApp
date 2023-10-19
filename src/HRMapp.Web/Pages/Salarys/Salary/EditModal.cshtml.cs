using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Salarys;
using HRMapp.Salarys.Dtos;
using HRMapp.Web.Pages.Salarys.Salary.ViewModels;

namespace HRMapp.Web.Pages.Salarys.Salary;

public class EditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditSalaryViewModel ViewModel { get; set; }

    private readonly ISalaryAppService _service;

    public EditModalModel(ISalaryAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<SalaryDto, CreateEditSalaryViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditSalaryViewModel, CreateUpdateSalaryDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}