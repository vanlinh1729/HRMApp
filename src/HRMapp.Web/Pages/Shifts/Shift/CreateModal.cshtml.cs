using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Shifts;
using HRMapp.Shifts.Dtos;
using HRMapp.Web.Pages.Shifts.Shift.ViewModels;

namespace HRMapp.Web.Pages.Shifts.Shift;

public class CreateModalModel : CreateOrEditModalModel
{

    private readonly IShiftAppService _service;

    public CreateModalModel(IShiftAppService service): base(service)
    {
        _service = service;
    }
}