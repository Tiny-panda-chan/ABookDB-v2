using Microsoft.EntityFrameworkCore;
using Models.Models;

public class ABookDBContext : DbContext
    {
        public ABookDBContext(DbContextOptions<ABookDBContext> options) : base(options)
        {

        }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ReadBooksModel> ReadBooks { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<UrlModel> BookUrls { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }

    }
