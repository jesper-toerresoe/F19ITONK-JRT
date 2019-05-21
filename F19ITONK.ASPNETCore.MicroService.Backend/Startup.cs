using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using F19ITONK.ASPNETCore.MicroService.Backend.Models;

namespace F19ITONK.ASPNETCore.MicroService.Backend
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
            //MSQL_SERVER2017_JRT_SERVICE_HOST
            var host = Configuration["MSQL_SERVER2017_JRT_SERVICE_HOST"];
            //host = "10.11.247.185";
            host = "mssql-server2017-jrt";
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            // https://stackoverflow.com/questions/26434738/how-do-you-really-serialize-circular-referencing-objects-with-newtonsoft-json
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Error);
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects);
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => options.AllowInputFormatterExceptionMessages = true);

            // Register the Swagger generator, defining 1 or more Swagger documents
            //https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "F19ITONK Haandvaerker API", Version = "v1.0" });
            });
            //    services.AddDbContext<BackendDBContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("BackendDBContext")));

            //services.AddDbContext<BackendDBContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("KubeBackendDBContext")));
            System.Console.WriteLine(" connectionstring Data Source=" + host + ";User ID=SA;Password=F19Itonk;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            services.AddDbContext<BackendDBContext>(options =>
                                options.UseSqlServer("Data Source=" + host + ";User ID=SA;Password=F19Itonk;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

            //           options.UseSqlServer("Data Source=" + host + ";Database=F19ITONK;User ID=SA;Password=F19Itonk;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            //Initial Catalog
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,BackendDBContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //https://stackoverflow.com/questions/50507668/database-migrate-creating-database-but-not-tables-ef-net-core-2
            db.Database.Migrate(); //Udfør migration fra koden
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //https://stackoverflow.com/questions/47101155/ambiguites-in-aspnet-core-2-0-using-swagger-in-web-api
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "F19ITONK Haandvaerker API V1");
            });
        }
    }
}
