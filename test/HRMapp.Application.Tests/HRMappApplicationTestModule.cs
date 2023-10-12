using Volo.Abp.Modularity;

namespace HRMapp;

[DependsOn(
    typeof(HRMappApplicationModule),
    typeof(HRMappDomainTestModule)
    )]
public class HRMappApplicationTestModule : AbpModule
{

}
