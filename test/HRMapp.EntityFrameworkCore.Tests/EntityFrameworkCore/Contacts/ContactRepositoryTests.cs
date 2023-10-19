using System;
using System.Threading.Tasks;
using HRMapp.Contacts;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace HRMapp.EntityFrameworkCore.Contacts;

public class ContactRepositoryTests : HRMappEntityFrameworkCoreTestBase
{
    private readonly IContactRepository _contactRepository;

    public ContactRepositoryTests()
    {
        _contactRepository = GetRequiredService<IContactRepository>();
    }

    /*
    [Fact]
    public async Task Test1()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            // Arrange

            // Act

            //Assert
        });
    }
    */
}
