using HRMapp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace HRMapp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HRMappEntityFrameworkCoreModule),
    typeof(HRMappApplicationContractsModule)
    )]
public class HRMappDbMigratorModule : AbpModule
{
}
