using HRMapp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HRMapp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HRMappController : AbpControllerBase
{
    protected HRMappController()
    {
        LocalizationResource = typeof(HRMappResource);
    }
}
