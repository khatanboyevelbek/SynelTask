using SynelTask.Web.Brokers.Loggings;
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

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<StorageBroker>();
            RegisterBrokers(builder.Services);
            RegisterFoundationServices(builder.Services);
            RegisterProcessingServices(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
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