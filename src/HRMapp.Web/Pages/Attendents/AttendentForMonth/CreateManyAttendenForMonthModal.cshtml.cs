using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth;

public class CreateManyAttendenForMonthModalModel: HRMappPageModel
{
    [BindProperty]
    public CreateManyAttendentForMonthViewModel ViewModel { get; set; }

    private readonly IAttendentForMonthAppService _service;

    public CreateManyAttendenForMonthModalModel(IAttendentForMonthAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        ViewModel = new CreateManyAttendentForMonthViewModel();
    }

    public virtual async Task OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateManyAttendentForMonthViewModel, CreateManyAttendentForMonthDto>(ViewModel);
        await _service.CreateManyAttendentForMonthAsync(dto);
    }
}