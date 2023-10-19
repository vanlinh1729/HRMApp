using HRMapp.Attendents;
using HRMapp.Salarys;
using HRMapp.Contracts;
using HRMapp.Contacts;
using HRMapp.Shifts;
using HRMapp.Departments;
using HRMapp.Employees;
using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace HRMapp.EntityFrameworkCore;

[DependsOn(
    typeof(HRMappDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
public class HRMappEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        HRMappEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<HRMappDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
            options.AddRepository<Employee, EmployeeRepository>();
            options.AddRepository<Department, DepartmentRepository>();
            options.AddRepository<Shift, ShiftRepository>();
            options.AddRepository<Contact, ContactRepository>();
            options.AddRepository<Contract, ContractRepository>();
            options.AddRepository<Salary, SalaryRepository>();
            options.AddRepository<Attendent, AttendentRepository>();
            options.AddRepository<AttendentLine, AttendentLineRepository>();
            options.AddRepository<AttendentForMonth, AttendentForMonthRepository>();
            options.AddRepository<EmployeeHistory, EmployeeHistoryRepository>();
        });

        Configure<AbpDbContextOptions>(options =>
        {
                /* The main point to change your DBMS.
                 * See also HRMappMigrationsDbContextFactory for EF Core tooling. */
            options.UseSqlServer();
        });

    }
}
