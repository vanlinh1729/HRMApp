using System;
using Volo.Abp.Domain.Repositories;

namespace HRMapp.Employees;

public interface IEmployeeHistoryRepository : IRepository<EmployeeHistory, Guid>
{
}
