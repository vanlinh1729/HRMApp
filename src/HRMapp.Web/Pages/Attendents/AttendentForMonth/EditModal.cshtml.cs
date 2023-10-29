using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth;

public class EditModalModel : CreateOrEditModalModel
{

    private readonly IAttendentForMonthAppService _service;

    public EditModalModel(IAttendentForMonthAppService service): base(service)
    {
        _service = service;
    }
}