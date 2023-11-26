using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HRMapp.EntityFrameworkCore;
using HRMapp.Localization;
using HRMapp.MultiTenancy;
using HRMapp.Web.Menus;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace HRMapp.Web;

[DependsOn(
    typeof(HRMappHttpApiModule),
    typeof(HRMappApplicationModule),
    typeof(HRMappEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpSettingManagementWebModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule)
    )]
public class HRMappWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        IWebHostEnvironment hostingEnvironment = context.Services.GetHostingEnvironment();

        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(HRMappResource),
                typeof(HRMappDomainModule).Assembly,
                typeof(HRMappDomainSharedModule).Assembly,
                typeof(HRMappApplicationModule).Assembly,
                typeof(HRMappApplicationContractsModule).Assembly,
                typeof(HRMappWebModule).Assembly
            );
        });
        
        if (!hostingEnvironment.IsDevelopment())
        {
            PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
            {
                options.AddDevelopmentEncryptionAndSigningCertificate = false;
            });
            PreConfigure<OpenIddictServerBuilder>(builder =>
            {
                // In production, it is recommended to use two RSA certificates, 
                // one for encryption, one for signing.
                builder.AddEncryptionCertificate(
                    GetEncryptionCertificate(hostingEnvironment, context.Services.GetConfiguration()));
                builder.AddSigningCertificate(
                    GetSigningCertificate(hostingEnvironment, context.Services.GetConfiguration()));
            });
        }
       
            PreConfigure<OpenIddictBuilder>(builder =>
            {
                builder.AddValidation(options =>
                {
                    options.AddAudiences("HRMapp");
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });
            });        
       
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = configuration.GetSection("GoogleClientID")["ID"];
                options.ClientSecret =  configuration.GetSection("GoogleClientID")["Key"];
            })
            .AddFacebook(facebook =>
            {
                facebook.AppId = configuration.GetSection("FacebookClientID")["ID"];
                facebook.AppSecret = configuration.GetSection("FacebookClientID")["Key"];
                facebook.Scope.Add("email");
                facebook.Scope.Add("public_profile");
            });

        
        ConfigureAuthentication(context);
        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureNavigationServices();
        ConfigureAutoApiControllers();
        ConfigureSwaggerServices(context.Services);
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
            options
                .ScriptBundles
                .Get(StandardBundles.Scripts.Global)
                .AddFiles("/libs/jspdf/jspdf.umd.min.js");
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<HRMappWebModule>();
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<HRMappDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}HRMapp.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<HRMappDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}HRMapp.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<HRMappApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}HRMapp.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<HRMappApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}HRMapp.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<HRMappWebModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureNavigationServices()
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new HRMappMenuContributor());
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(HRMappApplicationModule).Assembly);
        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "HRMapp API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "HRMapp API");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
    
    private X509Certificate2 GetSigningCertificate(IWebHostEnvironment hostingEnv,
                            IConfiguration configuration)
{
    var fileName = $"cert-signing.pfx";
    var passPhrase = configuration["MyAppCertificate:X590:PassPhrase"]; 
    var file = Path.Combine(hostingEnv.ContentRootPath, fileName);        
    if (File.Exists(file))
    {
        var created = File.GetCreationTime(file);
        var days = (DateTime.Now - created).TotalDays;
        if (days > 180)          
            File.Delete(file);
        else
            return new X509Certificate2(file, passPhrase,
                         X509KeyStorageFlags.MachineKeySet);
    }

    // file doesn't exist or was deleted because it expired
    using var algorithm = RSA.Create(keySizeInBits: 2048);
    var subject = new X500DistinguishedName("CN=Fabrikam Signing Certificate");
    var request = new CertificateRequest(subject, algorithm, 
                        HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    request.CertificateExtensions.Add(new X509KeyUsageExtension(
                        X509KeyUsageFlags.DigitalSignature, critical: true));
    var certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow, 
                        DateTimeOffset.UtcNow.AddYears(2));
    File.WriteAllBytes(file, certificate.Export(X509ContentType.Pfx, string.Empty));
    return new X509Certificate2(file, passPhrase, 
                        X509KeyStorageFlags.MachineKeySet);
}

private X509Certificate2 GetEncryptionCertificate(IWebHostEnvironment hostingEnv,
                             IConfiguration configuration)
{
    var fileName = $"cert-encryption.pfx";
    var passPhrase = configuration["MyAppCertificate:X590:PassPhrase"]; 
    var file = Path.Combine(hostingEnv.ContentRootPath, fileName);
    if (File.Exists(file))
    {
        var created = File.GetCreationTime(file);
        var days = (DateTime.Now - created).TotalDays;
        if (days > 180)
            File.Delete(file);
        else
            return new X509Certificate2(file, passPhrase, 
                            X509KeyStorageFlags.MachineKeySet);
    }

    // file doesn't exist or was deleted because it expired
    using var algorithm = RSA.Create(keySizeInBits: 2048);
    var subject = new X500DistinguishedName("CN=Fabrikam Encryption Certificate");
    var request = new CertificateRequest(subject, algorithm, 
                        HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    request.CertificateExtensions.Add(new X509KeyUsageExtension(
                        X509KeyUsageFlags.KeyEncipherment, critical: true));
    var certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow,
                        DateTimeOffset.UtcNow.AddYears(2));
    File.WriteAllBytes(file, certificate.Export(X509ContentType.Pfx, string.Empty));
    return new X509Certificate2(file, passPhrase, X509KeyStorageFlags.MachineKeySet);
}
}
