using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Contacts;
using HRMapp.Contacts.Dtos;
using HRMapp.Web.Pages.Contacts.Contact.ViewModels;

namespace HRMapp.Web.Pages.Contacts.Contact;

public class EditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateEditContactViewModel ViewModel { get; set; }

    private readonly IContactAppService _service;

    public EditModalModel(IContactAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<ContactDto, CreateEditContactViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateEditContactViewModel, CreateUpdateContactDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}