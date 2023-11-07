using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Salarys;
using HRMapp.Salarys.Dtos;
using HRMapp.Web.Pages.Salarys.Salary.ViewModels;

namespace HRMapp.Web.Pages.Salarys.Salary;

public class CreateManySalaryModalModel : HRMappPageModel
{
    [BindProperty]
    public CreateManySalaryViewModel ViewModel { get; set; }

    private readonly ISalaryAppService _service;

    public CreateManySalaryModalModel(ISalaryAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        ViewModel = new CreateManySalaryViewModel();
    }

    public virtual async Task OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateManySalaryViewModel, CreateManySalaryDto>(ViewModel);
        await _service.CreateManySalaryAsync(dto);
    }
}