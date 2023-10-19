using System;
using System.Threading.Tasks;
using HRMapp.Attendents;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HRMapp.EntityFrameworkCore.Attendents;

public class AttendentForMonthRepositoryTests : HRMappEntityFrameworkCoreTestBase
{
    private readonly IAttendentForMonthRepository _attendentForMonthRepository;

    public AttendentForMonthRepositoryTests()
    {
        _attendentForMonthRepository = GetRequiredService<IAttendentForMonthRepository>();
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
