using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.Attendent.ViewModels;

namespace HRMapp.Web.Pages.Attendents.Attendent;

public class CreateModalModel : CreateOrEditModalModel
{

    private readonly IAttendentAppService _service;

    public CreateModalModel(IAttendentAppService service): base(service)
    {
        _service = service;
    }
}