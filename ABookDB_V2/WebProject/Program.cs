using DBService;
using DBService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Models.Interfaces;
using Models.Models;
using WebProject.Consts;
using WebProject.Helpers;
using WebProject.Helpers.Interfaces;
using WebProject.ModelTranslator;
using WebProject.ViewModels.Book;
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
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IModelTranslatorBook, ModelTranslator.ModelTranslatorBook>();
            builder.Services.AddScoped<IModelTranslatorUser, ModelTranslator.ModelTranslatorUser>();
            builder.Services.AddScoped<IModelTranslatorCategory, ModelTranslator.ModelTranslatorCategory>();
            builder.Services.AddScoped<IModelTranslatorReview, ModelTranslator.ModelTranslatorReview>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IAuthHelper, AuthHelper>();
            /*builder.Services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<BookModel, DetailVM>();
                configuration.CreateMap<BookModel, EditVM>();
            });*/
            //builder.Services.AddScoped<IStatusService, StatusService>();

            builder.Services.AddAuthentication().AddCookie(ConstanceHelper.AuthCookie, o =>
            {
                o.LoginPath = "/User/Login";
                o.AccessDeniedPath = "/User/Naah";
                o.Cookie.Name = ConstanceHelper.AuthCookie;
            });
            builder.Services.AddAuthorization();
            

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
                name: "default",
                pattern: "{controller=Book}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
