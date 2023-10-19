using System;
using System.Threading.Tasks;
using HRMapp.Attendents;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HRMapp.EntityFrameworkCore.Attendents;

public class AttendentRepositoryTests : HRMappEntityFrameworkCoreTestBase
{
    private readonly IAttendentRepository _attendentRepository;

    public AttendentRepositoryTests()
    {
        _attendentRepository = GetRequiredService<IAttendentRepository>();
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
