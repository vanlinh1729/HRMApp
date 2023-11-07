using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Web;

[Dependency(ReplaceServices = true)]
public class HRMappBrandingProvider : DefaultBrandingProvider
{
    private readonly ICurrentTenant _currentTenant;

    public HRMappBrandingProvider(ICurrentTenant currentTenant)
    {
        _currentTenant = currentTenant;
    }

    public override string AppName => "Phần mềm quản lý nhân sự THGroup";
    
    
}
