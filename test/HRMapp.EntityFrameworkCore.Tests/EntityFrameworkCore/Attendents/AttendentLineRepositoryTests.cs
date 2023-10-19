using System;
using System.Threading.Tasks;
using HRMapp.Attendents;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HRMapp.EntityFrameworkCore.Attendents;

public class AttendentLineRepositoryTests : HRMappEntityFrameworkCoreTestBase
{
    private readonly IAttendentLineRepository _attendentLineRepository;

    public AttendentLineRepositoryTests()
    {
        _attendentLineRepository = GetRequiredService<IAttendentLineRepository>();
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
