using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Web.Pages.Attendents.Attendent.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMapp.Web.Pages.Attendents.Attendent;

public class CreateOrEditModalModel : HRMappPageModel
{
    private readonly IAttendentAppService _service;

    public CreateOrEditModalModel(IAttendentAppService service)
    {
        _service = service;
    }

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    public List<SelectListItem> Employees { get; set; }

    [BindProperty] public CreateEditAttendentViewModel ViewModel { get; set; }

    public virtual async Task OnGetAsync()
    {
        if (!IsCreate())
        {
            var dto = await _service.GetAsync(Id);
            /*
            ViewModel = ObjectMapper.Map<AttendentDto, CreateEditAttendentViewModel>(dto);
        */
        }
        else
        {
            ViewModel = new CreateEditAttendentViewModel();
        }

        var employees = await _service.GetListEmployeeAsync();
        Employees = employees.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        if (!IsCreate())
        {
            var dto = ObjectMapper.Map<CreateEditAttendentViewModel, CreateUpdateAttendentDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
        }
        else
        {
            var dto = ObjectMapper.Map<CreateEditAttendentViewModel, CreateUpdateAttendentDto>(ViewModel);
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