using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Localization;
using HRMapp.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace HRMapp.Web.Menus;

public class HRMappMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<HRMappResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                HRMappMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );
        
         context.Menu.AddItem(
         new ApplicationMenuItem(HRMappMenus.Department, l["Menu:Department"],
         "/Departments/Department", icon: "fas fa-building",
         requiredPermissionName: HRMappPermissions.Department.Default, order:2));
 context.Menu.AddItem(   
     new ApplicationMenuItem(HRMappMenus.Employee, l["Menu:Employee"],
         "/Employees/Employee", icon: "fas fa-user-circle",
         requiredPermissionName: HRMappPermissions.Employee.Default, order:3)
         .AddItem(new ApplicationMenuItem(HRMappMenus.AttendentLine, l["Menu:Employee"],
             "~/Employees/Employee", requiredPermissionName: HRMappPermissions.Employee.Default))
         .AddItem(new ApplicationMenuItem(HRMappMenus.EmployeeHistory, l["Menu:EmployeeHistory"],
             "~/Employees/EmployeeHistory", requiredPermissionName: HRMappPermissions.Employee.Default)));
 context.Menu.AddItem(
     new ApplicationMenuItem(HRMappMenus.Shift, l["Menu:Shift"], "/Shifts/Shift",
         icon: "fa fa-clock-o", requiredPermissionName: HRMappPermissions.Shift.Default, order: 4));
 context.Menu.AddItem(
     new ApplicationMenuItem(HRMappMenus.Attendent, l["Menu:Attendent"], "/Attendents/Attendent",
             icon: "fas fa-business-time", requiredPermissionName: HRMappPermissions.Attendent.Default, order: 5)
         .AddItem(new ApplicationMenuItem(HRMappMenus.Attendent, l["Menu:Attendent"],
             "~/Attendents/Attendent", requiredPermissionName: HRMappPermissions.Attendent.Default)) 
         /*.AddItem(new ApplicationMenuItem(HRMappMenus.AttendentLine, l["Menu:AttendentLine"],
             "~/Attendents/AttendentLine", requiredPermissionName: HRMappPermissions.Employee.Default))*/
         .AddItem(new ApplicationMenuItem(HRMappMenus.Attendent, l["Menu:AttendentForMonth"],
             "/Attendents/AttendentForMonth", requiredPermissionName: HRMappPermissions.Attendent.Default)));
     
 context.Menu.AddItem(
     new ApplicationMenuItem(HRMappMenus.Salary, l["Menu:Salary"], "/Salarys/Salary",
         icon: "fas fa-table", requiredPermissionName: HRMappPermissions.Salary.Default, order:6)
         );
 context.Menu.AddItem(
     new ApplicationMenuItem(HRMappMenus.Contact, l["Menu:Contact"], "/Contacts/Contact",
         icon: "fa fa-address-book-o", requiredPermissionName: HRMappPermissions.Contact.Default, order:7)
 );
 context.Menu.AddItem(
     new ApplicationMenuItem(HRMappMenus.Contract, l["Menu:Contract"], "/Contracts/Contract",
         icon: "fa fa-folder-open", requiredPermissionName: HRMappPermissions.Contract.Default, order:8)
 );
 context.Menu.AddItem(
     new ApplicationMenuItem(HRMappMenus.BackupAndRestore, l["Menu:BackupAndRestore"], "/Backups/Backup",
         icon: "fa fa-cog", requiredPermissionName: HRMappPermissions.Attendent.Default, order:9)
 );
 

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
        // if (await context.IsGrantedAsync(HRMappPermissions.Employee.Default))
        // {
        //     context.Menu.AddItem(
        //         new ApplicationMenuItem(HRMappMenus.Employee, l["Menu:Employee"], "/Employees/Employee")
        //     );
        // }
        // if (await context.IsGrantedAsync(HRMappPermissions.Department.Default))
        // {
        //     context.Menu.AddItem(
        //         new ApplicationMenuItem(HRMappMenus.Department, l["Menu:Department"], "/Departments/Department")
        //     );
        // }
        // if (await context.IsGrantedAsync(HRMappPermissions.Shift.Default))
        // {
        //     context.Menu.AddItem(
        //         new ApplicationMenuItem(HRMappMenus.Shift, l["Menu:Shift"], "/Shifts/Shift")
        //     );
        // }
        // if (await context.IsGrantedAsync(HRMappPermissions.Contact.Default))
        // {
        //     context.Menu.AddItem(
        //         new ApplicationMenuItem(HRMappMenus.Contact, l["Menu:Contact"], "/Contacts/Contact")
        //     );
        // }
        // if (await context.IsGrantedAsync(HRMappPermissions.Contract.Default))
        // {
        //     context.Menu.AddItem(
        //         new ApplicationMenuItem(HRMappMenus.Contract, l["Menu:Contract"], "/Contracts/Contract")
        //     );
        // }
        // if (await context.IsGrantedAsync(HRMappPermissions.Salary.Default))
        // {
        //     context.Menu.AddItem(
        //         new ApplicationMenuItem(HRMappMenus.Salary, l["Menu:Salary"], "/Salarys/Salary")
        //     );
        // }
        // if (await context.IsGrantedAsync(HRMappPermissions.Attendent.Default))
        // {
        //     context.Menu.AddItem(
        //         new ApplicationMenuItem(HRMappMenus.Attendent, l["Menu:Attendent"], "/Attendents/Attendent")
        //     );
        // }
        // if (await context.IsGrantedAsync(HRMappPermissions.AttendentLine.Default))
        // {
        //     context.Menu.AddItem(
        //         new ApplicationMenuItem(HRMappMenus.AttendentLine, l["Menu:AttendentLine"], "/Attendents/AttendentLine")
        //     );
        // }
        // if (await context.IsGrantedAsync(HRMappPermissions.AttendentForMonth.Default))
        // {
        //     context.Menu.AddItem(
        //         new ApplicationMenuItem(HRMappMenus.AttendentForMonth, l["Menu:AttendentForMonth"], "/Attendents/AttendentForMonth")
        //     );
        // }
        // if (await context.IsGrantedAsync(HRMappPermissions.EmployeeHistory.Default))
        // {
        //     context.Menu.AddItem(
        //         new ApplicationMenuItem(HRMappMenus.EmployeeHistory, l["Menu:EmployeeHistory"], "/Employees/EmployeeHistory")
        //     );
        // }
    }
}
