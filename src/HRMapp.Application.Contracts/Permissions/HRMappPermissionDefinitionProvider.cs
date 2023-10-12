using HRMapp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HRMapp.Permissions;

public class HRMappPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HRMappPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(HRMappPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HRMappResource>(name);
    }
}
