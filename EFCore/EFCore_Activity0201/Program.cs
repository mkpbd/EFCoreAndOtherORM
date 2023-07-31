using EFCore_DBLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EFCore_Activity0201
{
    internal class Program
    {
        private static DbContextOptionsBuilder<ApplicationDbContext> _optionsBuilder;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

    

        static void BuildOptions()
        {
            _optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //_optionsBuilder.UseSqlServer(configuration.GetConnectionString( "AdventureWorks"));
        }
    }
}