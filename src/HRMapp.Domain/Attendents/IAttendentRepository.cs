using System;
using Volo.Abp.Domain.Repositories;

namespace HRMapp.Attendents;

public interface IAttendentRepository : IRepository<Attendent, Guid>
{
}
