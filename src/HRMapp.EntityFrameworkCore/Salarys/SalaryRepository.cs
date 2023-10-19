using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HRMapp.Salarys;

public class SalaryRepository : EfCoreRepository<HRMappDbContext, Salary, Guid>, ISalaryRepository
{
    public SalaryRepository(IDbContextProvider<HRMappDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Salary>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}