using System;
using Volo.Abp.Application.Dtos;

namespace HRMapp.Contacts.Dtos;

[Serializable]
public class ContactDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; }

    public Gender Gender { get; set; }

    public DateTime? BirthDay { get; set; }

    public bool Active { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }
    public string Education { get; set; }
    public string Language { get; set; }
}