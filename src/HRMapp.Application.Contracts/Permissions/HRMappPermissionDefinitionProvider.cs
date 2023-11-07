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

        var employeePermission = myGroup.AddPermission(HRMappPermissions.Employee.Default, L("Permission:Employee"));
        employeePermission.AddChild(HRMappPermissions.Employee.Create, L("Permission:Create"));
        employeePermission.AddChild(HRMappPermissions.Employee.Update, L("Permission:Update"));
        employeePermission.AddChild(HRMappPermissions.Employee.Delete, L("Permission:Xoa"));

        var departmentPermission = myGroup.AddPermission(HRMappPermissions.Department.Default, L("Permission:Department"));
        departmentPermission.AddChild(HRMappPermissions.Department.Create, L("Permission:Create"));
        departmentPermission.AddChild(HRMappPermissions.Department.Update, L("Permission:Update"));
        departmentPermission.AddChild(HRMappPermissions.Department.Delete, L("Permission:Delete"));

        var shiftPermission = myGroup.AddPermission(HRMappPermissions.Shift.Default, L("Permission:Shift"));
        shiftPermission.AddChild(HRMappPermissions.Shift.Create, L("Permission:Create"));
        shiftPermission.AddChild(HRMappPermissions.Shift.Update, L("Permission:Update"));
        shiftPermission.AddChild(HRMappPermissions.Shift.Delete, L("Permission:Delete"));

        var contactPermission = myGroup.AddPermission(HRMappPermissions.Contact.Default, L("Permission:Contact"));
        contactPermission.AddChild(HRMappPermissions.Contact.Create, L("Permission:Create"));
        contactPermission.AddChild(HRMappPermissions.Contact.Update, L("Permission:Update"));
        contactPermission.AddChild(HRMappPermissions.Contact.Delete, L("Permission:Delete"));

        var contractPermission = myGroup.AddPermission(HRMappPermissions.Contract.Default, L("Permission:Contract"));
        contractPermission.AddChild(HRMappPermissions.Contract.Create, L("Permission:Create"));
        contractPermission.AddChild(HRMappPermissions.Contract.Update, L("Permission:Update"));
        contractPermission.AddChild(HRMappPermissions.Contract.Delete, L("Permission:Delete"));

        var salaryPermission = myGroup.AddPermission(HRMappPermissions.Salary.Default, L("Permission:Salary"));
        salaryPermission.AddChild(HRMappPermissions.Salary.Create, L("Permission:Create"));
        salaryPermission.AddChild(HRMappPermissions.Salary.Update, L("Permission:Update"));
        salaryPermission.AddChild(HRMappPermissions.Salary.Delete, L("Permission:Delete"));

        var attendentPermission = myGroup.AddPermission(HRMappPermissions.Attendent.Default, L("Permission:Attendent"));
        attendentPermission.AddChild(HRMappPermissions.Attendent.Create, L("Permission:Create"));
        attendentPermission.AddChild(HRMappPermissions.Attendent.Update, L("Permission:Update"));
        attendentPermission.AddChild(HRMappPermissions.Attendent.Delete, L("Permission:Delete"));

        var attendentLinePermission = myGroup.AddPermission(HRMappPermissions.AttendentLine.Default, L("Permission:AttendentLine"));
        attendentLinePermission.AddChild(HRMappPermissions.AttendentLine.Create, L("Permission:Create"));
        attendentLinePermission.AddChild(HRMappPermissions.AttendentLine.Update, L("Permission:Update"));
        attendentLinePermission.AddChild(HRMappPermissions.AttendentLine.Delete, L("Permission:Delete"));

        var attendentForMonthPermission = myGroup.AddPermission(HRMappPermissions.AttendentForMonth.Default, L("Permission:AttendentForMonth"));
        attendentForMonthPermission.AddChild(HRMappPermissions.AttendentForMonth.Create, L("Permission:Create"));
        attendentForMonthPermission.AddChild(HRMappPermissions.AttendentForMonth.Update, L("Permission:Update"));
        attendentForMonthPermission.AddChild(HRMappPermissions.AttendentForMonth.Delete, L("Permission:Delete"));

        var employeeHistoryPermission = myGroup.AddPermission(HRMappPermissions.EmployeeHistory.Default, L("Permission:EmployeeHistory"));
        employeeHistoryPermission.AddChild(HRMappPermissions.EmployeeHistory.Create, L("Permission:Create"));
        employeeHistoryPermission.AddChild(HRMappPermissions.EmployeeHistory.Update, L("Permission:Update"));
        employeeHistoryPermission.AddChild(HRMappPermissions.EmployeeHistory.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HRMappResource>(name);
    }
}
