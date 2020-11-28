using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using VueCliMiddleware;
using Microsoft.AspNetCore.Mvc;
using Esp8266VueMeteo.Database;
using Esp8266VueMeteo.Services;
using Esp8266VueMeteo.Repositories;
using Microsoft.Extensions.Hosting;

namespace Esp8266VueMeteo
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
            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddSingleton(appSettings);

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddControllersWithViews().AddNewtonsoftJson();

            // In production, the Vue files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "clientapp/dist";
            });
            services.AddDbContext<MeteoDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            services.AddTransient<ISensorUpdateService, SensorUpdateService>();
            services.AddTransient<IDevicesService, DevicesService>();
            services.AddTransient<IMeasurementsService, MeasurementsService>();
            services.AddTransient<IJsonUpdatesService, JsonUpdatesService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAqiEcoService, AqiEcoService>();

            services.AddTransient<IDevicesRepository, DevicesRepository>();
            services.AddTransient<IMeasurementsRepository, MeasurementsRepository>();
            services.AddTransient<IJsonUpdatesRepository, JsonUpdatesRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseSpaStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                    spa.Options.SourcePath = "clientapp";
                else
                    spa.Options.SourcePath = "dist";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve", port: 8080);
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                }
            });
        }
    }
}
