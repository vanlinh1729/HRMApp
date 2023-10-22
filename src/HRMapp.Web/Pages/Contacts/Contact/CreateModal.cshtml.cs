using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Contacts;
using HRMapp.Contacts.Dtos;
using HRMapp.Web.Pages.Contacts.Contact.ViewModels;

namespace HRMapp.Web.Pages.Contacts.Contact;

public class CreateModalModel : CreateOrEditModalModel
{

    private readonly IContactAppService _service;

    public CreateModalModel(IContactAppService service): base(service)
    {
        _service = service;
    }
}