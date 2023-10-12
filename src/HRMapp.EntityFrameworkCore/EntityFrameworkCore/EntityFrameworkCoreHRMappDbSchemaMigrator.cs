using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HRMapp.Data;
using Volo.Abp.DependencyInjection;

namespace HRMapp.EntityFrameworkCore;

public class EntityFrameworkCoreHRMappDbSchemaMigrator
    : IHRMappDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreHRMappDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the HRMappDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<HRMappDbContext>()
            .Database
            .MigrateAsync();
    }
}
