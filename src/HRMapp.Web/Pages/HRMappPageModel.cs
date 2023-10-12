using HRMapp.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace HRMapp.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class HRMappPageModel : AbpPageModel
{
    protected HRMappPageModel()
    {
        LocalizationResourceType = typeof(HRMappResource);
    }
}
