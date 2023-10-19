using System;
using Volo.Abp.Domain.Repositories;

namespace HRMapp.Attendents;

public interface IAttendentForMonthRepository : IRepository<AttendentForMonth, Guid>
{
}
