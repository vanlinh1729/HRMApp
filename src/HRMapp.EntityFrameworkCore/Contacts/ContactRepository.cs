using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HRMapp.Contacts;

public class ContactRepository : EfCoreRepository<HRMappDbContext, Contact, Guid>, IContactRepository
{
    public ContactRepository(IDbContextProvider<HRMappDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Contact>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}