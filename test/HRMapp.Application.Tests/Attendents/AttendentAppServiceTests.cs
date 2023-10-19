using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace HRMapp.Attendents;

public class AttendentAppServiceTests : HRMappApplicationTestBase
{
    private readonly IAttendentAppService _attendentAppService;

    public AttendentAppServiceTests()
    {
        _attendentAppService = GetRequiredService<IAttendentAppService>();
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

