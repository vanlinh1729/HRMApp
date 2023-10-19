using System;
using System.Threading.Tasks;
using HRMapp.Salarys;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HRMapp.EntityFrameworkCore.Salarys;

public class SalaryRepositoryTests : HRMappEntityFrameworkCoreTestBase
{
    private readonly ISalaryRepository _salaryRepository;

    public SalaryRepositoryTests()
    {
        _salaryRepository = GetRequiredService<ISalaryRepository>();
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
