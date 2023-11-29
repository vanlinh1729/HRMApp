using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Permissions;
using HRMapp.Web.Pages.Attendents.Attendent.ViewModels;
using HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;
using HRMapp.Web.Pages.Salarys.Salary.ViewModels;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth;

public class ViewAllAttendentForMonthForDepartmentModalModel : ExportAllAttendentForMonthModalModel
{

    private readonly IAttendentForMonthAppService _service;

    public ViewAllAttendentForMonthForDepartmentModalModel(IAttendentForMonthAppService service) : base(service)
    {
        _service = service;
    }
}