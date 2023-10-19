using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HRMapp.Contacts;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace HRMapp.Web.Pages.Contacts.Contact;

public class IndexModel : HRMappPageModel
{
    public ContactFilterInput ContactFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class ContactFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ContactName")]
    public string? Name { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ContactGender")]
    public Gender? Gender { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ContactBirthDay")]
    public DateTime? BirthDay { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ContactActive")]
    public bool? Active { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ContactEmail")]
    public string? Email { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ContactPhoneNumber")]
    public string? PhoneNumber { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "ContactAddress")]
    public string? Address { get; set; }
}
