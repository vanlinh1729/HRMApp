using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace HRMapp.Contracts;

public class ContractAppServiceTests : HRMappApplicationTestBase
{
    private readonly IContractAppService _contractAppService;

    public ContractAppServiceTests()
    {
        _contractAppService = GetRequiredService<IContractAppService>();
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

