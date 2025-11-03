using DBService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Models.Models;
using WebProject.ModelTranslator;
using WebProject.ViewModels;
namespace WebProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ABookDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefConStr"),
                x => x.MigrationsAssembly("DBService")));
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IModelTranslator, ModelTranslator.ModelTranslator>();
            builder.Services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<BookModel, DetailVM>();
            });




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
