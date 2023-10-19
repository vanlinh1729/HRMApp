using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace HRMapp.Salarys;

public class SalaryAppServiceTests : HRMappApplicationTestBase
{
    private readonly ISalaryAppService _salaryAppService;

    public SalaryAppServiceTests()
    {
        _salaryAppService = GetRequiredService<ISalaryAppService>();
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

