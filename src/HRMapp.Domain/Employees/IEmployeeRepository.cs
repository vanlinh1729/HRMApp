using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace HRMapp.Employees;

public interface IEmployeeRepository : IRepository<Employee, Guid>
{
    Task<int> UpdateDepartment(Guid Id, [CanBeNull]Guid? newDepartmentId);

}
