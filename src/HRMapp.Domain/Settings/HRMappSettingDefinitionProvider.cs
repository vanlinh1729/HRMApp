using Volo.Abp.Settings;

namespace HRMapp.Settings;

public class HRMappSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(HRMappSettings.MySetting1));
    }
}
