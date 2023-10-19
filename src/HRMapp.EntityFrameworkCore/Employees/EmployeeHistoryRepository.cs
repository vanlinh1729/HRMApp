using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HRMapp.Employees;

public class EmployeeHistoryRepository : EfCoreRepository<HRMappDbContext, EmployeeHistory, Guid>, IEmployeeHistoryRepository
{
    public EmployeeHistoryRepository(IDbContextProvider<HRMappDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<EmployeeHistory>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}