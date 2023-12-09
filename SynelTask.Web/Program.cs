using SynelTask.Web.Brokers.Loggins;
using SynelTask.Web.Brokers.Storages;
using SynelTask.Web.Services.Foundations.Employees;
using SynelTask.Web.Services.Processings.Employees;

namespace SynelTask.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<StorageBroker>();
            RegisterBrokers(builder.Services);
            RegisterFoundationServices(builder.Services);
            RegisterProcessingServices(builder.Services);
            AddCors(builder.Services);

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void AddCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithMethods("GET", "PUT", "DELETE", "POST", "PATCH"));
            });
        }

        private static void RegisterBrokers(IServiceCollection services)
        {
            services.AddTransient<IStorageBroker, StorageBroker>();
            services.AddTransient<ILogginBroker, LogginBroker>();
        }
        private static void RegisterFoundationServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
        }

        private static void RegisterProcessingServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeeProcessingService, EmployeeProcessingService>();
        }
    }
}