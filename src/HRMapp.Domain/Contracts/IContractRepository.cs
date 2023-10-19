using System;
using Volo.Abp.Domain.Repositories;

namespace HRMapp.Contracts;

public interface IContractRepository : IRepository<Contract, Guid>
{
}
