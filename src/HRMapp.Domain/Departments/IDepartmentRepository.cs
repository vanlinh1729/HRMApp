using System;
using Volo.Abp.Domain.Repositories;

namespace HRMapp.Departments;

public interface IDepartmentRepository : IRepository<Department, Guid>
{
}
