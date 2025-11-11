using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Microsoft.AspNetCore.Identity;

public class ABookDBContext : DbContext
    {
        public ABookDBContext(DbContextOptions<ABookDBContext> options) : base(options)
        {

        }

        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ReadBooksModel> ReadBooks { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<UrlModel> BookUrls { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSeeding((context, _) =>
        {
            if (!context.Set<CategoryModel>().Any(c => c.Name != null))
            {
                context.Set<CategoryModel>().AddRange(
                    new () { Name = "Fantasy"}, 
                    new () { Name = "Adventure"},
                    new () { Name = "System"}
                    );
                context.SaveChanges();
            }

            if (!context.Set<UserModel>().Any(u => u.Email != null))
            {
                UserModel user = new UserModel()
                {
                    Email = "isma@gmail.com",
                    Username = "isma"
                };
                user.Password = new PasswordHasher<UserModel>().HashPassword(user, "isma");
                context.Set<UserModel>().Add(user);
                context.SaveChanges();
            }
        }
        );
        base.OnConfiguring(optionsBuilder);
    }

    }
