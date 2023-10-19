using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using HRMapp.Employees;
using Volo.Abp.EntityFrameworkCore.Modeling;
using HRMapp.Departments;
using HRMapp.Shifts;
using HRMapp.Contacts;
using HRMapp.Contracts;
using HRMapp.Salarys;
using HRMapp.Attendents;
using HRMapp.Users;
using Volo.Abp.Identity.Settings;

namespace HRMapp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class HRMappDbContext :
    AbpDbContext<HRMappDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Salary> Salaries { get; set; }
    public DbSet<Attendent> Attendents { get; set; }
    public DbSet<AttendentLine> AttendentLines { get; set; }
    public DbSet<AttendentForMonth> AttendentForMonths { get; set; }
    public DbSet<EmployeeHistory> EmployeeHistories { get; set; }
    public DbSet<HrmUser> HrmUsers { get; set; }

    public HRMappDbContext(DbContextOptions<HRMappDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(HRMappConsts.DbTablePrefix + "YourEntities", HRMappConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});


        builder.Entity<Employee>(b =>
        {
            b.ToTable(HRMappConsts.DbTablePrefix + "Employees", HRMappConsts.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<Department>(b =>
        {
            b.ToTable(HRMappConsts.DbTablePrefix + "Departments", HRMappConsts.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<Shift>(b =>
        {
            b.ToTable(HRMappConsts.DbTablePrefix + "Shifts", HRMappConsts.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<Contact>(b =>
        {
            b.ToTable(HRMappConsts.DbTablePrefix + "Contacts", HRMappConsts.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<Contract>(b =>
        {
            b.ToTable(HRMappConsts.DbTablePrefix + "Contracts", HRMappConsts.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<Salary>(b =>
        {
            b.ToTable(HRMappConsts.DbTablePrefix + "Salaries", HRMappConsts.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<Attendent>(b =>
        {
            b.ToTable(HRMappConsts.DbTablePrefix + "Attendents", HRMappConsts.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<AttendentLine>(b =>
        {
            b.ToTable(HRMappConsts.DbTablePrefix + "AttendentLines", HRMappConsts.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<AttendentForMonth>(b =>
        {
            b.ToTable(HRMappConsts.DbTablePrefix + "AttendentForMonths", HRMappConsts.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });


        builder.Entity<EmployeeHistory>(b =>
        {
            b.ToTable(HRMappConsts.DbTablePrefix + "EmployeeHistories", HRMappConsts.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });
        builder.Entity<HrmUser>(b =>
        {
            b.ToTable(HRMappConsts.DbTablePrefix + "HrmUsers", HRMappConsts.DbSchema);
            b.ConfigureByConvention();

        });
    }
}
