using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.Attendent.ViewModels;

namespace HRMapp.Web.Pages.Attendents.Attendent;


public class EditModalModel : CreateOrEditModalModel
{
    private readonly IAttendentAppService _service;

    public EditModalModel(IAttendentAppService service): base(service)
    {
        _service = service;
    }
}
