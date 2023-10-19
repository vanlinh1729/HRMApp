using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HRMapp.Attendents;

public class AttendentRepository : EfCoreRepository<HRMappDbContext, Attendent, Guid>, IAttendentRepository
{
    public AttendentRepository(IDbContextProvider<HRMappDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Attendent>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}