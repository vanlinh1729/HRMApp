using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace HRMapp.Shifts;

public class ShiftAppServiceTests : HRMappApplicationTestBase
{
    private readonly IShiftAppService _shiftAppService;

    public ShiftAppServiceTests()
    {
        _shiftAppService = GetRequiredService<IShiftAppService>();
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

