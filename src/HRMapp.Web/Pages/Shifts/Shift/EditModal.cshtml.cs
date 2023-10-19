using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Shifts;
using HRMapp.Shifts.Dtos;
using HRMapp.Web.Pages.Shifts.Shift.ViewModels;

namespace HRMapp.Web.Pages.Shifts.Shift;

public class EditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditShiftViewModel ViewModel { get; set; }

    private readonly IShiftAppService _service;

    public EditModalModel(IShiftAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<ShiftDto, CreateEditShiftViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditShiftViewModel, CreateUpdateShiftDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}