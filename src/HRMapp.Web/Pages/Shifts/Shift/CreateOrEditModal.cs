using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using HRMapp.Shifts;
using HRMapp.Shifts.Dtos;
using HRMapp.Web.Pages.Shifts.Shift.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMapp.Web.Pages.Shifts.Shift;
public class CreateOrEditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    [BindProperty]
    public CreateEditShiftViewModel ViewModel { get; set; }

    private readonly IShiftAppService _service;

    public CreateOrEditModalModel(IShiftAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        if (!IsCreate())
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<ShiftDto, CreateEditShiftViewModel>(dto);
        }
        else
        {
            ViewModel = new CreateEditShiftViewModel();
        }
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        if (!IsCreate())
        {
            var dto = ObjectMapper.Map<CreateEditShiftViewModel, CreateUpdateShiftDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
        }
        else
        {
            var dto = ObjectMapper.Map<CreateEditShiftViewModel, CreateUpdateShiftDto>(ViewModel);
            await _service.CreateAsync(dto);
        }
        return NoContent();
    }
    
    bool IsCreate()
    {
        if (Id != Guid.Empty)
        {
            return false;
        }
        return true;
    }
}