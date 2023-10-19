using System;
using Volo.Abp.Domain.Repositories;

namespace HRMapp.Attendents;

public interface IAttendentLineRepository : IRepository<AttendentLine, Guid>
{
}
