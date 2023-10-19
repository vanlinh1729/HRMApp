using System;
using System.Threading.Tasks;
using HRMapp.Departments;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HRMapp.EntityFrameworkCore.Departments;

public class DepartmentRepositoryTests : HRMappEntityFrameworkCoreTestBase
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentRepositoryTests()
    {
        _departmentRepository = GetRequiredService<IDepartmentRepository>();
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
