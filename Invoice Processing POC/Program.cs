using DAL.Database;
using Microsoft.EntityFrameworkCore;
using Services.BusinessLogic.Implementation;
using Services.BusinessLogic.Interface;
using Services.Login.Implementation;
using Services.Login.Interface;

namespace Invoice_Processing_POC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var ConnectionString = builder.Configuration.GetConnectionString("dbcs");
            builder.Services.AddDbContext<InvoiceProcessingDbContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });
            builder.Services.AddTransient<ILoginInterface, LoginClass>();
            builder.Services.AddTransient<IUploadFile, UploadFile>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //app.UseExceptionHandler("/Home/Error");
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
            
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello World");
            //    await next();
            //});
            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Middleware 1 Before");

            //    await next();

            //    //Console.WriteLine("Middleware 1 After");
            //});
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
