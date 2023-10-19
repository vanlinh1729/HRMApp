using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Contacts;
using HRMapp.Contacts.Dtos;
using HRMapp.Web.Pages.Contacts.Contact.ViewModels;

namespace HRMapp.Web.Pages.Contacts.Contact;

public class CreateModalModel : HRMappPageModel
{
    [BindProperty]
    public CreateEditContactViewModel ViewModel { get; set; }

    private readonly IContactAppService _service;

    public CreateModalModel(IContactAppService service)
    {
        _service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditContactViewModel, CreateUpdateContactDto>(ViewModel);
        await _service.CreateAsync(dto);
        return NoContent();
    }
}