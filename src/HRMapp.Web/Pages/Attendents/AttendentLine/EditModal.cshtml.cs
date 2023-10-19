using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.AttendentLine.ViewModels;

namespace HRMapp.Web.Pages.Attendents.AttendentLine;

public class EditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditAttendentLineViewModel ViewModel { get; set; }

    private readonly IAttendentLineAppService _service;

    public EditModalModel(IAttendentLineAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<AttendentLineDto, CreateEditAttendentLineViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditAttendentLineViewModel, CreateUpdateAttendentLineDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}