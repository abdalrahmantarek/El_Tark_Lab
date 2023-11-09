//using DinkToPdf;
//using DinkToPdf.Contracts;
using LAB.IService;
using LAB.Models;
using LAB.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using LAB.Data;


namespace LAB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("LABContextConnection") ?? throw new InvalidOperationException("Connection string 'LABContextConnection' not found.");

			builder.Services.AddDbContext<LABContext>(options =>
				options.UseSqlServer(connectionString)); ;




			builder.Services.AddDefaultIdentity<IdentityUser>(options =>
			{
				options.SignIn.RequireConfirmedAccount = false;
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;


			}).AddEntityFrameworkStores<LABContext>();


			// Add services to the container.
			builder.Services.AddControllersWithViews();
			IConfiguration configuration = new ConfigurationBuilder()
	        .AddJsonFile("appsettings.json")
	        .Build();

			//Inject Db Context
			builder.Services.AddDbContext<HMISContext>(option => option.UseSqlServer(configuration.GetConnectionString("LABContextConnection")));

			//To Access Logged user
			builder.Services.AddHttpContextAccessor();


			builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


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

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllerRoute(
				name: "Admin",
				pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();

			app.Run();
        }
    }
}