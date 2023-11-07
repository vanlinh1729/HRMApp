using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Permissions;
using HRMapp.Web.Pages.Attendents.Attendent.ViewModels;
using HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;
using HRMapp.Web.Pages.Salarys.Salary.ViewModels;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth;

public class ExportAllAttendentForMonthModalModel : HRMappPageModel
{
    [BindProperty]
    public AllAttendentForMonthDto ViewModel { get; set; } 
    [BindProperty]
    public CreateManyAttendentForMonthViewModel ViewMonthModel { get; set; }

    private readonly IAttendentForMonthAppService _service;

    public ExportAllAttendentForMonthModalModel(IAttendentForMonthAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        ViewMonthModel = new CreateManyAttendentForMonthViewModel();
    }
    public virtual async Task OnPostAsync()
    {
        var dto = ObjectMapper
            .Map<CreateManyAttendentForMonthViewModel, CreateManyAttendentForMonthDto>(ViewMonthModel);
        ViewModel = await _service.GetListManyAttendentForMonthAsync(dto);
    }
}