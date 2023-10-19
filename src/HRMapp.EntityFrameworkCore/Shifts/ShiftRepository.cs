using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HRMapp.Shifts;

public class ShiftRepository : EfCoreRepository<HRMappDbContext, Shift, Guid>, IShiftRepository
{
    public ShiftRepository(IDbContextProvider<HRMappDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Shift>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}