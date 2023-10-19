using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HRMapp.Attendents;

public class AttendentForMonthRepository : EfCoreRepository<HRMappDbContext, AttendentForMonth, Guid>, IAttendentForMonthRepository
{
    public AttendentForMonthRepository(IDbContextProvider<HRMappDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<AttendentForMonth>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}