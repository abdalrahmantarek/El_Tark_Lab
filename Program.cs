//using DinkToPdf;
//using DinkToPdf.Contracts;
using LAB.IService;
using LAB.Models;
using LAB.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LAB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


             builder.Services.AddDbContext<HMISContext>(option => option.UseSqlServer("Data Source=.;Initial Catalog=LAB;Integrated Security=True;TrustServerCertificate=true;"));


            builder.Services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
			//builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));


			builder.Services.AddAutoMapper(typeof(Program));



			
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
    }
}