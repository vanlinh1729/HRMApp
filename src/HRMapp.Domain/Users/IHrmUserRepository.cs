using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HRMapp.Users;

public interface IHrmUserRepository : IRepository<HrmUser, Guid>
{
    Task<List<HrmUser>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default);
}
