using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace SwashBuckleWebApiOData
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning (options => options.ReportApiVersions = true);
            services.AddControllers ();
            services.AddOData ().EnableApiVersioning();
            services.AddODataApiExplorer (options=> {
                options.UseQualifiedNames = true;
                options.GroupNameFormat = "'v'VVV";

                options.SubstituteApiVersionInUrl = true;
            });

            services.AddMvc (options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter> ().Where (x => x.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add (new MediaTypeHeaderValue (@"application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter> ().Where (x => x.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add (new MediaTypeHeaderValue (@"application/prs.odatatestxx-odata"));
                }
                ;
                options.EnableEndpointRouting = false;
            });

            services.AddSwaggerGen ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, VersionedODataModelBuilder modelBuilder,
            IApiVersionDescriptionProvider provider, IWebHostEnvironment env)
        {
            if (env.IsDevelopment ())
            {
                app.UseDeveloperExceptionPage ();
            }

            app.UseRouting ();
            app.UseApiVersioning ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints =>
            {
                endpoints.MapControllers ();
            });

            app.UseMvc (builder => {                
                var edmModels = modelBuilder.GetEdmModels ();
                builder.MapVersionedODataRoutes ("odata", "odata", edmModels);
            });
            app.UseSwagger ();
            app.UseSwaggerUI (options => {
                // build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint ($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant ());
                }
            });
        }
    }
}
