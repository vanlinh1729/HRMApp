using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Salarys;
using HRMapp.Salarys.Dtos;
using HRMapp.Web.Pages.Employees.Employee.ViewModels;
using HRMapp.Web.Pages.Salarys.Salary.ViewModels;

namespace HRMapp.Web.Pages.Salarys.Salary;

public class ExportSalaryForMonthModel: HRMappPageModel
{
    [BindProperty]
    public SalaryForMonthDto ViewModel { get; set; }
    [BindProperty]
    public CreateManySalaryViewModel ViewMonthModel { get; set; }

    private readonly ISalaryAppService _service;

    public ExportSalaryForMonthModel(ISalaryAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        ViewMonthModel = new CreateManySalaryViewModel();
    }

    public virtual async Task OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateManySalaryViewModel, CreateManySalaryDto>(ViewMonthModel);
        ViewModel = await _service.GetListSalaryForMonthAsync(dto);
    }
}