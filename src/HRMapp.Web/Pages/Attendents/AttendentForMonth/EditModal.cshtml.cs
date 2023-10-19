using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth;

public class EditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditAttendentForMonthViewModel ViewModel { get; set; }

    private readonly IAttendentForMonthAppService _service;

    public EditModalModel(IAttendentForMonthAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<AttendentForMonthDto, CreateEditAttendentForMonthViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditAttendentForMonthViewModel, CreateUpdateAttendentForMonthDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}