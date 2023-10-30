using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.AttendentForMonth.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMapp.Web.Pages.Attendents.AttendentForMonth;

public class CreateOrEditModalModel : HRMappPageModel
{
    private readonly IAttendentForMonthAppService _service;

    public CreateOrEditModalModel(IAttendentForMonthAppService service)
    {
        _service = service;
    }

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public List<SelectListItem> Employees { get; set; }

    [BindProperty] public CreateEditAttendentForMonthViewModel ViewModel { get; set; }

    public virtual async Task OnGetAsync()
    {
        if (!IsCreate())
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<AttendentForMonthDto, CreateEditAttendentForMonthViewModel>(dto);
        }
        else
        {
            ViewModel = new CreateEditAttendentForMonthViewModel();
        }

        var employees = await _service.GetListEmployeeAsync();
        Employees = employees.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        if (!IsCreate())
        {
            var dto = ObjectMapper.Map<CreateEditAttendentForMonthViewModel, CreateUpdateAttendentForMonthDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
        }
        else
        {
            var dto = ObjectMapper.Map<CreateEditAttendentForMonthViewModel, CreateUpdateAttendentForMonthDto>(ViewModel);
            await _service.CreateAsync(dto);
        }

        return NoContent();
    }

    private bool IsCreate()
    {
        if (Id != Guid.Empty)
        {
            return false;
        }

        return true;
    }
}