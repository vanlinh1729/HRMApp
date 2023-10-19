using System;
using System.Threading.Tasks;
using HRMapp.Employees;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HRMapp.EntityFrameworkCore.Employees;

public class EmployeeHistoryRepositoryTests : HRMappEntityFrameworkCoreTestBase
{
    private readonly IEmployeeHistoryRepository _employeeHistoryRepository;

    public EmployeeHistoryRepositoryTests()
    {
        _employeeHistoryRepository = GetRequiredService<IEmployeeHistoryRepository>();
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
