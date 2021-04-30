// ******************************
// Article BlazorSpread
// ******************************
using BlazorCosmosDB.Server.Services;
using BlazorCosmosDB.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorCosmosDB.Server
{
    public class Startup
    {
        // for static files
        public static string PATH { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            // cosmos db services
            //! Warning. Error if CosmoDB Emulator is stoped
            services.AddSingleton<ICosmosService<Movie>>(DatabaseInitializer.Initialize<Movie>(Configuration).GetAwaiter().GetResult());
            // other entities has the same pattern, except the type..
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            PATH = env.ContentRootPath;

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
