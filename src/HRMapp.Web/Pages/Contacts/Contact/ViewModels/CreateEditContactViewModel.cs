using System;
using System.ComponentModel.DataAnnotations;
using HRMapp.Contacts;

namespace HRMapp.Web.Pages.Contacts.Contact.ViewModels;

public class CreateEditContactViewModel
{
    [Display(Name = "ContactName")]
    public string Name { get; set; }

    [Display(Name = "ContactGender")]
    public Gender Gender { get; set; }

    [Display(Name = "ContactBirthDay")]
    public DateTime? BirthDay { get; set; }

    [Display(Name = "ContactActive")]
    public bool Active { get; set; }

    [Display(Name = "ContactEmail")]
    public string Email { get; set; }

    [Display(Name = "ContactPhoneNumber")]
    public string PhoneNumber { get; set; }

    [Display(Name = "ContactAddress")]
    public string Address { get; set; }
}
