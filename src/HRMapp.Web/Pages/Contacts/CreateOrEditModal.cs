using System;
using System.Threading.Tasks;
using HRMapp.Contacts;
using HRMapp.Contacts.Dtos;
using HRMapp.Web.Pages.Contacts.Contact.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HRMapp.Web.Pages.Contacts;

public class CreateOrEditModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    [BindProperty]
    public CreateEditContactViewModel ViewModel { get; set; }

    private readonly IContactAppService _service;

    public CreateOrEditModalModel(IContactAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        if (!IsCreate())
        {
            var dto = await _service.GetAsync(Id);
            
            ViewModel = ObjectMapper.Map<ContactDto, CreateEditContactViewModel>(dto);
        }
        else
        {
            ViewModel = new CreateEditContactViewModel();
        }
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        if (!IsCreate())
        {
            var dto = ObjectMapper.Map<CreateEditContactViewModel, CreateUpdateContactDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
        }
        else
        {
            var dto = ObjectMapper.Map<CreateEditContactViewModel, CreateUpdateContactDto>(ViewModel);
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