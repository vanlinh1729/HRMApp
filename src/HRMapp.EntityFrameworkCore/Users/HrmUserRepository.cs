using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using HRMapp.Data;
using HRMapp.Employees;
using HRMapp.EntityFrameworkCore;
using HRMapp.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users.EntityFrameworkCore;


namespace HRMapp.EntityFrameworkCore
{
    public class HrmUserRepository : EfCoreRepository<HRMappDbContext, HrmUser, Guid>, IHrmUserRepository
    {
        public HrmUserRepository(IDbContextProvider<HRMappDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public async Task<List<HrmUser>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.UserName.Contains(filter))
                .Take(maxCount).ToListAsync(cancellationToken);
        }
    }
}
