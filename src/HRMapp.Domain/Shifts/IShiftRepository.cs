using System;
using Volo.Abp.Domain.Repositories;

namespace HRMapp.Shifts;

public interface IShiftRepository : IRepository<Shift, Guid>
{
}
