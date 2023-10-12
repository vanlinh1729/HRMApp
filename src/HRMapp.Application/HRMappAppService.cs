using System;
using System.Collections.Generic;
using System.Text;
using HRMapp.Localization;
using Volo.Abp.Application.Services;

namespace HRMapp;

/* Inherit your application services from this class.
 */
public abstract class HRMappAppService : ApplicationService
{
    protected HRMappAppService()
    {
        LocalizationResource = typeof(HRMappResource);
    }
}
