using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace HRMapp.Employees;

public class EmployeeHistoryAppServiceTests : HRMappApplicationTestBase
{
    private readonly IEmployeeHistoryAppService _employeeHistoryAppService;

    public EmployeeHistoryAppServiceTests()
    {
        _employeeHistoryAppService = GetRequiredService<IEmployeeHistoryAppService>();
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

