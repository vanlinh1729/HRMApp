using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Shifts;
using HRMapp.Shifts.Dtos;
using HRMapp.Web.Pages.Shifts.Shift.ViewModels;

namespace HRMapp.Web.Pages.Shifts.Shift;

public class CreateModalModel : HRMappPageModel
{
    [BindProperty]
    public CreateEditShiftViewModel ViewModel { get; set; }

    private readonly IShiftAppService _service;

    public CreateModalModel(IShiftAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditShiftViewModel, CreateUpdateShiftDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}