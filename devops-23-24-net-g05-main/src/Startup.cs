using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Oogarts.Data;
using Oogarts.Services;
using MySql.Data.MySqlClient;

namespace Oogarts
{
    public class Startup
    {
        public IConfiguration Configuration { get; }  // Inject IConfiguration

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            string connectionString = "Server=localhost;Database=oogarts;User=root;Password=root;";

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<TeamMemberService>();
            services.AddScoped<PatientService>();
            services.AddScoped<AppointmentService>();
            services.AddScoped<SpecializationService>();
            services.AddScoped<UserService>();
            services.AddScoped<EyeConditionService>();
            //services.AddScoped<RoleService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DbInitializer.Initialize(context);
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
                
            }

            DbInitializer.Initialize(context);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
