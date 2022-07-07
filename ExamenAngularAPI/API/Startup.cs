using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Http;
using Examen_Ingreso.Servicios;
using Examen_Ingreso.Interfaces;
using Examen_Ingreso.ContextosDB;


namespace Examen_Ingreso
{
    /// <summary>
    /// Clase inicial de la aplicación
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// propiedad que obtiene la configuración inicial
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Metodo para realizar la configuración inicial
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //se habilitan los cors
            services.AddCors();
            services.AddAutoMapper(typeof(Startup));

            //se adicionan servicios
            services.AddTransient<IDatos, DatosServicio>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddDbContext<ContextoDbExamen>((options) => {
                    options.UseSqlite(Configuration.GetConnectionString("dbExamenIngresoConnectionString"));
            });
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API-Examen-Ingreso Notas",
                    Description = "Api para realizar el examen de ingreso"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                if (!String.IsNullOrEmpty(Configuration["ConfiguracionParametros:DirectorioVirtual"])){
                    c.SwaggerEndpoint("/" + Configuration["ConfiguracionParametros:DirectorioVirtual"]  + "/swagger/v1/swagger.json", "Api_Servicios_Tesoreria-V1");
                }else{
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Examen_Ingreso-V1");
                }
                
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            ////habilitando CORS
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        /// <summary>
        /// clase que sirve para mostrar la documentación swagger
        /// </summary>
        public class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
        {
            /// <summary>
            /// metodo que sirve para mostrar la documentación swagger
            /// </summary>
            public void Apply(ControllerModel controller)
            {
                // Ejemplo: "Controllers.V1"
                var controllerNamespace = controller.ControllerType.Namespace;
                var apiVersion = controllerNamespace.Split('.').Last().ToLower();
                controller.ApiExplorer.GroupName = apiVersion;
            }
        }
    }
}
