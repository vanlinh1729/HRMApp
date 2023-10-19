using System;
using System.Threading.Tasks;
using HRMapp.Employees;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HRMapp.EntityFrameworkCore.Employees;

public class EmployeeRepositoryTests : HRMappEntityFrameworkCoreTestBase
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeRepositoryTests()
    {
        _employeeRepository = GetRequiredService<IEmployeeRepository>();
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
