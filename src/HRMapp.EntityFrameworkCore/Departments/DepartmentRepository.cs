using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HRMapp.Departments;

public class DepartmentRepository : EfCoreRepository<HRMappDbContext, Department, Guid>, IDepartmentRepository
{
    public DepartmentRepository(IDbContextProvider<HRMappDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Department>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}