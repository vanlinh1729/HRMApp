using HRMapp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HRMapp;

[DependsOn(
    typeof(HRMappEntityFrameworkCoreTestModule)
    )]
public class HRMappDomainTestModule : AbpModule
{

}
