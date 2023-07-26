using EFCoreSecondConsoleApp.Data;
using EFCoreSecondConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Services
{
    public class ExampleService
    {
        private ApplicationDbContext _context;
      
        public ExampleService()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            _context = context;

        }
     

        public void ExampleSaveEntry()
        {
            try
            {
                var items = new ExampleEntity { Name = "Test One" };

                _context.Add(items);
                _context.SaveChanges();
                Console.WriteLine("Data has been Save");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data not save");
            }
        }

    }
}
