using System;
using System.Threading.Tasks;
using HRMapp.Contacts;
using HRMapp.Contacts.Dtos;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Employees.Employee.ViewModels;

namespace HRMapp.Web.Pages.Contacts.Contact;

public class ViewModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    [BindProperty]
    public ContactDto ViewModel { get; set; }

    private readonly IContactAppService _service;

    public ViewModalModel(IContactAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = dto;
    }
}
