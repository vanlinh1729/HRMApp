using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Salarys;
using HRMapp.Salarys.Dtos;
using HRMapp.Web.Pages.Employees.Employee.ViewModels;
using HRMapp.Web.Pages.Salarys.Salary.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMapp.Web.Pages.Salarys.Salary;

public class ExportSalaryForMonthForDepartmentModel: HRMappPageModel
{
    [BindProperty]
    public SalaryForMonthDto ViewModel { get; set; }
    public List<SelectListItem> Departments { get; set; }

    [BindProperty]
    public CreateSalaryForDepartmentViewModel ViewDepartmentModel { get; set; }

    private readonly ISalaryAppService _service;
    private readonly IEmployeeAppService _employeeAppService;

    public ExportSalaryForMonthForDepartmentModel(ISalaryAppService service,IEmployeeAppService employeeAppService)
    {
        _employeeAppService = employeeAppService;
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        ViewDepartmentModel = new CreateSalaryForDepartmentViewModel();
        var departments = await _employeeAppService.GetListDepartmentAsync();
        Departments = departments.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public virtual async Task OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateSalaryForDepartmentViewModel, CreateManySalaryForMonthDto>(ViewDepartmentModel);
        ViewModel = await _service.GetListSalaryForMonthForDepartmentAsync(dto);
    }
}