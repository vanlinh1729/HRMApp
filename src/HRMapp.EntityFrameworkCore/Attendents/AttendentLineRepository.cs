using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HRMapp.Attendents;

public class AttendentLineRepository : EfCoreRepository<HRMappDbContext, AttendentLine, Guid>, IAttendentLineRepository
{
    public AttendentLineRepository(IDbContextProvider<HRMappDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<AttendentLine>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}