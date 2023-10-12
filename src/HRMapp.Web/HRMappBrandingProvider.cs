using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace HRMapp.Web;

[Dependency(ReplaceServices = true)]
public class HRMappBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "HRMapp";
}
