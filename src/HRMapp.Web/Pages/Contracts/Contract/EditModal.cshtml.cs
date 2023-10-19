using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Contracts;
using HRMapp.Contracts.Dtos;
using HRMapp.Web.Pages.Contracts.Contract.ViewModels;

namespace HRMapp.Web.Pages.Contracts.Contract;

public class EditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditContractViewModel ViewModel { get; set; }

    private readonly IContractAppService _service;

    public EditModalModel(IContractAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<ContractDto, CreateEditContractViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditContractViewModel, CreateUpdateContractDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}