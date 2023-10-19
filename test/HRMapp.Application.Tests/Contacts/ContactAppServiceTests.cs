using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace HRMapp.Contacts;

public class ContactAppServiceTests : HRMappApplicationTestBase
{
    private readonly IContactAppService _contactAppService;

    public ContactAppServiceTests()
    {
        _contactAppService = GetRequiredService<IContactAppService>();
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

