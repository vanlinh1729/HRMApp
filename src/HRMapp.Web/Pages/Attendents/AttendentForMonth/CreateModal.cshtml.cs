using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth;

public class CreateModalModel : CreateOrEditModalModel
{

    private readonly IAttendentForMonthAppService _service;

    public CreateModalModel(IAttendentForMonthAppService service): base(service)
    {
        _service = service;
    }
}