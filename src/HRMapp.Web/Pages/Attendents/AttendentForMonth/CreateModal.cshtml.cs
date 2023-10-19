using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth;

public class CreateModalModel : HRMappPageModel
{
    [BindProperty]
    public CreateEditAttendentForMonthViewModel ViewModel { get; set; }

    private readonly IAttendentForMonthAppService _service;

    public CreateModalModel(IAttendentForMonthAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditAttendentForMonthViewModel, CreateUpdateAttendentForMonthDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}