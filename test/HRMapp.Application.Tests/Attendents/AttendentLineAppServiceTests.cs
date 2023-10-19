using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace HRMapp.Attendents;

public class AttendentLineAppServiceTests : HRMappApplicationTestBase
{
    private readonly IAttendentLineAppService _attendentLineAppService;

    public AttendentLineAppServiceTests()
    {
        _attendentLineAppService = GetRequiredService<IAttendentLineAppService>();
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

