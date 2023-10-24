using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Contracts;
using HRMapp.Contracts.Dtos;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Contracts.Contract.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMapp.Web.Pages.Contracts.Contract;

public class CreateOrEditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    [BindProperty]
    public CreateEditContractViewModel ViewModel { get; set; }

    private readonly IContractAppService _service;
    public List<SelectListItem> Employees { get; set; }

    public CreateOrEditModalModel(IContractAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        if (!IsCreate())
        {
            var dto = await _service.GetAsync(Id);
            
            ViewModel = ObjectMapper.Map<ContractDto, CreateEditContractViewModel>(dto);
        }
        else
        {
            ViewModel = new CreateEditContractViewModel();
        }

        var employees = await _service.GetListEmployees();
        Employees = employees.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString()))
            .ToList();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        if (!IsCreate())
        {
            var dto = ObjectMapper.Map<CreateEditContractViewModel, CreateUpdateContractDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
        }
        else
        {
            var dto = ObjectMapper.Map<CreateEditContractViewModel, CreateUpdateContractDto>(ViewModel);
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