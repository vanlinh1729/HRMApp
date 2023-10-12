using System.Threading.Tasks;

namespace HRMapp.Data;

public interface IHRMappDbSchemaMigrator
{
    Task MigrateAsync();
}
