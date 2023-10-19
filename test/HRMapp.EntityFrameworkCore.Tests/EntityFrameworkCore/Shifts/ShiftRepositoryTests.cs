using System;
using System.Threading.Tasks;
using HRMapp.Shifts;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HRMapp.EntityFrameworkCore.Shifts;

public class ShiftRepositoryTests : HRMappEntityFrameworkCoreTestBase
{
    private readonly IShiftRepository _shiftRepository;

    public ShiftRepositoryTests()
    {
        _shiftRepository = GetRequiredService<IShiftRepository>();
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
