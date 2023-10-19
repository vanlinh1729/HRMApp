using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Users;

namespace HRMapp.Users;

public class HrmUser : AggregateRoot<Guid>, IUser
{
    
    public Guid? TenantId { get; set; }
    public string UserName { get; }

    [CanBeNull]
    public string Email { get; }

    [CanBeNull]
    public string Name { get; }

    [CanBeNull]
    public string Surname { get; }

    public bool IsActive { get; }

    public bool EmailConfirmed { get; }

    [CanBeNull]
    public string PhoneNumber { get; }

    public bool PhoneNumberConfirmed { get; }
    

}
