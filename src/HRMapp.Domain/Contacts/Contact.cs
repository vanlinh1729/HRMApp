using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Contacts;

public class Contact: FullAuditedEntity<Guid>,IMultiTenant
{
    public  Guid? TenantId { get; set; }
    
    [Required]
    public string Name { get; set; }
    public Gender Gender { get; set; }
    [CanBeNull]
    public DateTime? BirthDay { get; set; }
    public bool Active { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    public Contact(Guid id, Guid? tenantId, string name, Gender gender, DateTime? birthDay, bool active, string email, string phoneNumber, string address) : base(id)
    {
        TenantId = tenantId;
        Name = name;
        Gender = gender;
        BirthDay = birthDay;
        Active = active;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
    }

   

    protected Contact()
    {
    }
}
