using System;
using Volo.Abp.Domain.Repositories;

namespace HRMapp.Contacts;

public interface IContactRepository : IRepository<Contact, Guid>
{
}
