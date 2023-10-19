using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Contracts;
using HRMapp.Contracts.Dtos;
using HRMapp.Web.Pages.Contracts.Contract.ViewModels;

namespace HRMapp.Web.Pages.Contracts.Contract;

public class CreateModalModel : HRMappPageModel
{
    [BindProperty]
    public CreateEditContractViewModel ViewModel { get; set; }

    private readonly IContractAppService _service;

    public CreateModalModel(IContractAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditContractViewModel, CreateUpdateContractDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}