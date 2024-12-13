//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Microsoft.Playwright.Services.Authorization.Common;
using Microsoft.Playwright.Services.Authorization.Models;
using Newtonsoft.Json.Converters;
using Serilog;
using System.Diagnostics;
using System.Reflection;

namespace Microsoft.Playwright.Services.Authorization
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
               // .AddOData(options => options.EnableQueryFeatures().AddRouteComponents("accounts/{accountId}/access-tokens", EdmModelProvider.GetEdmModel()));
                .AddOData(options => options.EnableQueryFeatures().AddRouteComponents("accounts/{accountId}", EdmModelProvider.GetEdmModel()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(AuthorizationServiceConstants.APIVersion1_0,
                    new OpenApiInfo()
                    {
                        Title = AuthorizationServiceConstants.ServiceName,
                        Version = AuthorizationServiceConstants.APIVersion1_0,
                    });
                c.EnableAnnotations();
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider sp)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/1.0/swagger.json", "PlaywrightService.Authorization API"));
            }

            app.UseCertificateForwarding();
            app.UseODataRouteDebug();
            app.UseMiddleware<ODataResponseModifierMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                endpoints.MapControllers();
            });   
        }
    }
}
