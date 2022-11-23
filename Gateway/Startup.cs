using System;
using System.Text.Json.Serialization;
using LT.DigitalOffice.Gateway.Clients.AdminServiceClients;
using LT.DigitalOffice.Gateway.Clients.AdminServiceClients.Interfaces;
using LT.DigitalOffice.Gateway.Clients.AuthServiceClients;
using LT.DigitalOffice.Gateway.Clients.AuthServiceClients.Interfaces;
using LT.DigitalOffice.Gateway.Clients.EmailServiceClients;
using LT.DigitalOffice.Gateway.Clients.EmailServiceClients.Interfaces;
using LT.DigitalOffice.Gateway.Clients.FeedbackServiceClients;
using LT.DigitalOffice.Gateway.Clients.FeedbackServiceClients.Interfaces;
using LT.DigitalOffice.Kernel.Configurations;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Middlewares.ApiInformation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace LT.DigitalOffice.Gateway
{
  public class Startup : BaseApiInfo
  {
    public const string CorsPolicyName = "LtDoCorsPolicy";

    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      BaseServiceInfoConfig serviceInfoConfig = Configuration
        .GetSection(BaseServiceInfoConfig.SectionName)
        .Get<BaseServiceInfoConfig>();

      Version = "1.0.0.0";
      Description = "Gateway is a pattern for a single entry point to the program.";
      StartTime = DateTime.UtcNow;
      ApiName = $"LT Digital Office - {serviceInfoConfig.Name}";
    }

    #region public methods

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddTransient<IAdminControllerClient, AdminControllerClient>();
      services.AddTransient<IGraphicalUserInterfaceControllerClient, GraphicalUserInterfaceControllerClient>();
      services.AddTransient<IModuleSettingControllerClient, ModuleSettingControllerClient>();
      services.AddTransient<IUnsentEmailController, UnsentEmailController>();
      services.AddTransient<IAuthControllerClient, AuthControllerClient>();
      services.AddTransient<IFeedbackControllerClient, FeedbackControllerClient>();

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

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway", Version = "1.0.0.0", });
      });
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      app.UseSwagger();

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway");
      });

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
