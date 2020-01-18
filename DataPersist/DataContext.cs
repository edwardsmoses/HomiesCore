using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataPersist
{

    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HomiesCoreDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new DataContext(builder.Options);
        }
    }

    public class DataContext : IdentityDbContext<Domain.AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Domain.Category> Categories { get; set; }
        public DbSet<Domain.Food> Foods { get; set; }
        public DbSet<Domain.FoodPicture> FoodPictures { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}
