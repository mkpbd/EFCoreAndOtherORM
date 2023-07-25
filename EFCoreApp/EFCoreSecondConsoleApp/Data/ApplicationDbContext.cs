using EFCoreSecondConsoleApp.DataSeeding;
using EFCoreSecondConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        private string connection = @"Server=SERVER\MSSQLSERVER01;Database=EFCoreDB;Trusted_Connection=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorDataSeedingConfiguration());
            modelBuilder.ApplyConfiguration(new ConfigurationBookData());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Author { get; set; }
    }
}
