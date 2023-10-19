using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.Attendent.ViewModels;

namespace HRMapp.Web.Pages.Attendents.Attendent;

public class EditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditAttendentViewModel ViewModel { get; set; }

    private readonly IAttendentAppService _service;

    public EditModalModel(IAttendentAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<AttendentDto, CreateEditAttendentViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditAttendentViewModel, CreateUpdateAttendentDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}