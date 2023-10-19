using System;
using Volo.Abp.Domain.Repositories;

namespace HRMapp.Salarys;

public interface ISalaryRepository : IRepository<Salary, Guid>
{
}
