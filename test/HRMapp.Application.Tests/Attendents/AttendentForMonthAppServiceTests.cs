using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace HRMapp.Attendents;

public class AttendentForMonthAppServiceTests : HRMappApplicationTestBase
{
    private readonly IAttendentForMonthAppService _attendentForMonthAppService;

    public AttendentForMonthAppServiceTests()
    {
        _attendentForMonthAppService = GetRequiredService<IAttendentForMonthAppService>();
    }

    /*
    [Fact]
    public async Task Test1()
    {
        // Arrange

        // Act

        // Assert
    }
    */
}

