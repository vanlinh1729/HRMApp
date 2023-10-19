using System;
using System.Threading.Tasks;
using HRMapp.Contracts;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HRMapp.EntityFrameworkCore.Contracts;

public class ContractRepositoryTests : HRMappEntityFrameworkCoreTestBase
{
    private readonly IContractRepository _contractRepository;

    public ContractRepositoryTests()
    {
        _contractRepository = GetRequiredService<IContractRepository>();
    }

    /*
    [Fact]
    public async Task Test1()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            // Arrange

            // Act

            //Assert
        });
    }
    */
}
