using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HRMapp.Contracts;

public class ContractRepository : EfCoreRepository<HRMappDbContext, Contract, Guid>, IContractRepository
{
    public ContractRepository(IDbContextProvider<HRMappDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Contract>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}