using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HRMapp.Data;

/* This is used if database provider does't define
 * IHRMappDbSchemaMigrator implementation.
 */
public class NullHRMappDbSchemaMigrator : IHRMappDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
