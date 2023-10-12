using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace HRMapp.Pages;

public class Index_Tests : HRMappWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
