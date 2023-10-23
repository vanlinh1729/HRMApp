using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Employees.Employee.ViewModels;

namespace HRMapp.Web.Pages.Employees.EmployeeHistory;

public class ViewModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    [BindProperty]
    public EmployeeHistoryDto ViewModel { get; set; }

    private readonly IEmployeeHistoryAppService _service;

    public ViewModalModel(IEmployeeHistoryAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetEmployeeHistoryDetail(Id);
        ViewModel = dto;
    }
}
