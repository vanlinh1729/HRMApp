using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Shifts;
using HRMapp.Shifts.Dtos;
using HRMapp.Web.Pages.Employees.Employee.ViewModels;

namespace HRMapp.Web.Pages.Shifts.Shift;

public class ViewModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    [BindProperty]
    public ShiftDto ViewModel { get; set; }

    private readonly IShiftAppService _service;

    public ViewModalModel(IShiftAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        if (dto != null)
            ViewModel = dto;
    }
}