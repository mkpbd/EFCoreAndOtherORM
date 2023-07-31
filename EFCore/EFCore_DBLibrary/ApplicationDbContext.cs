using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore_DBLibrary
{
    public class ApplicationDbContext : DbContext
    {
        private static IConfigurationRoot _configuration;

        //Add a default constructor if scaffolding is needed
        public ApplicationDbContext() { }
        //Add the complex constructor for allowing Dependency Injection
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //intentionally empty.
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true,
                    reloadOnChange: true);
                _configuration = builder.Build();


                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog= InventoryManagerDb; Trusted_Connection = True");
            }
        }

    }
}