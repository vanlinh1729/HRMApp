using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.Attendent.ViewModels;

namespace HRMapp.Web.Pages.Attendents.Attendent;

public class CreateModalModel : HRMappPageModel
{
    [BindProperty]
    public CreateEditAttendentViewModel ViewModel { get; set; }

    private readonly IAttendentAppService _service;

    public CreateModalModel(IAttendentAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditAttendentViewModel, CreateUpdateAttendentDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}