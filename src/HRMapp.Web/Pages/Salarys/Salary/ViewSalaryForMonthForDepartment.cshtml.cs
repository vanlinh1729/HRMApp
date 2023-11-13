using System;
using System.Threading.Tasks;
using HRMapp.Employees;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Salarys;
using HRMapp.Salarys.Dtos;
using HRMapp.Web.Pages.Salarys.Salary.ViewModels;

namespace HRMapp.Web.Pages.Salarys.Salary;

public class ViewSalaryForMonthForDepartmentModel : ExportSalaryForMonthForDepartmentModel
{

    private readonly ISalaryAppService _service;
    private readonly IEmployeeAppService _employeeAppService;

    public ViewSalaryForMonthForDepartmentModel(ISalaryAppService service,IEmployeeAppService employeeAppService): base(service,employeeAppService)
    {
        _service = service;
        _employeeAppService = employeeAppService;
    }
}