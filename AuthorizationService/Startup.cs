//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter.Deserialization;
using Microsoft.AspNetCore.OData.Formatter.Serialization;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Microsoft.Playwright.Services.Authorization.Common;
using Microsoft.Playwright.Services.Authorization.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
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
                .AddOData(options => options.EnableQueryFeatures().AddRouteComponents("accounts/{accountId}", EdmModelProvider.GetEdmModel()))
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Remove(manager.FeatureProviders.OfType<ControllerFeatureProvider>().FirstOrDefault());
                    manager.FeatureProviders.Add(new RemoveMetadataControllerFeatureProvider());
                });
                //.AddOData(options => options.EnableQueryFeatures().AddRouteComponents("accounts/{accountId}", EdmModelProvider.GetEdmModel(), builder => builder.AddSingleton<ODataSimplifiedOptions>(oDataSimplifiedOptions)));
                //.AddOData(options => options.EnableQueryFeatures().AddRouteComponents("accounts/{accountId}", EdmModelProvider.GetEdmModel(), builder => builder.AddSingleton<IODataSerializerProvider, CustomODataSerializerProvider>().AddSingleton<IODataDeserializerProvider, CustomODataDeserializerProvider>()));
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
            //services.AddSingleton<ODataSerializerProvider, CustomODataSerializerProvider>();
            //services.AddSingleton<ODataDeserializerProvider, CustomODataDeserializerProvider>();
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
            app.UseRouting();
            app.UseMiddleware<ODataResponseModifierMiddleware>();
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

        public class RemoveMetadataControllerFeatureProvider : ControllerFeatureProvider
{
        protected override bool IsController(TypeInfo typeInfo)
        {
            if (typeInfo.FullName == typeof(MetadataController).FullName)
            {
                return false;
            }

            return base.IsController(typeInfo);
        }
}
    }
}
