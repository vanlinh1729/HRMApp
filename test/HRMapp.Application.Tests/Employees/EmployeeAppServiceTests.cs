using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace HRMapp.Employees;

public class EmployeeAppServiceTests : HRMappApplicationTestBase
{
    private readonly IEmployeeAppService _employeeAppService;

    public EmployeeAppServiceTests()
    {
        _employeeAppService = GetRequiredService<IEmployeeAppService>();
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

