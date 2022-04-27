using System.Text.Json.Serialization;
using LT.DigitalOffice.AdminService.Client;
using LT.DigitalOffice.AdminService.Client.Interfaces;
using LT.DigitalOffice.AdminService.Data.Interfaces;
using LT.DigitalOffice.EmailService.Client;
using LT.DigitalOffice.EmailService.Client.Interfaces;
using LT.DigitalOffice.EmailService.Data.Interfaces;
using LT.DigitalOffice.Kernel.Configurations;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Middlewares.ApiInformation;

namespace Gateway
{

  public class Startup : BaseApiInfo
  {
    public const string CorsPolicyName = "LtDoCorsPolicy";

    private readonly BaseServiceInfoConfig _serviceInfoConfig;

    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      _serviceInfoConfig = Configuration
        .GetSection(BaseServiceInfoConfig.SectionName)
        .Get<BaseServiceInfoConfig>();
    }

    #region public methods

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddHttpClient<IAdminControllerClient, AdminControllerClient>(x => x.BaseAddress = new Uri("http://localhost:9838"));
      services.AddTransient<IAdminControllerClient, AdminControllerClient>();
      services.AddTransient<IGraphicalUserInterfaceControllerClient, GraphicalUserInterfaceControllerClient>();
      services.AddTransient<IModuleSettingControllerClient, ModuleSettingControllerClient>();
      services.AddTransient<IUnsentEmailController, UnsentEmailController>();

      services.AddCors(options =>
      {
        options.AddPolicy(
          CorsPolicyName,
          builder =>
          {
            builder
              .AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
          });
      });

      services.Configure<BaseServiceInfoConfig>(Configuration.GetSection(BaseServiceInfoConfig.SectionName));

      services.AddHttpContextAccessor();
      services
        .AddControllers()
        .AddJsonOptions(options =>
        {
          options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        })
        .AddNewtonsoftJson();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      app.UseForwardedHeaders();

      app.UseExceptionsHandler(loggerFactory);

      app.UseApiInformation();

      app.UseRouting();

      app.UseCors(CorsPolicyName);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers().RequireCors(CorsPolicyName);
      });
    }

    #endregion
  }
}
