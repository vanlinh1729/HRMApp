using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.AttendentLine.ViewModels;

namespace HRMapp.Web.Pages.Attendents.AttendentLine;

public class CreateModalModel : HRMappPageModel
{
    [BindProperty]
    public CreateEditAttendentLineViewModel ViewModel { get; set; }

    private readonly IAttendentLineAppService _service;

    public CreateModalModel(IAttendentLineAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditAttendentLineViewModel, CreateUpdateAttendentLineDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}