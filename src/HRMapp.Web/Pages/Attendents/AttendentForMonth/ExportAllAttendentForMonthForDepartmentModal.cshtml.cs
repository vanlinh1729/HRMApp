using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Permissions;
using HRMapp.Web.Pages.Attendents.Attendent.ViewModels;
using HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;
using HRMapp.Web.Pages.Salarys.Salary.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth;

public class ExportAllAttendentForMonthForDepartmentModalModel : HRMappPageModel
{
    [BindProperty]
    public AllAttendentForMonthDto ViewModel { get; set; } 
    [BindProperty]
    public CreateManyAttendentForMonthForDepartmentViewModel ViewMonthAndDepartmentModel { get; set; }
    public List<SelectListItem> Departments { get; set; }


    private readonly IAttendentForMonthAppService _service;

    public ExportAllAttendentForMonthForDepartmentModalModel(IAttendentForMonthAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        ViewMonthAndDepartmentModel = new CreateManyAttendentForMonthForDepartmentViewModel();
        var departments = await _service.GetListDepartmentAsync();
        Departments = departments.Items.Select(x => new SelectListItem(x.Name, x.Name))
            .ToList();
    }
    public virtual async Task OnPostAsync()
    {        var departments = await _service.GetListDepartmentAsync();

        Departments = departments.Items.Select(x => new SelectListItem(x.Name, x.Name))
            .ToList();
    }
}