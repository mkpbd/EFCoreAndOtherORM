using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreConsoleApp.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string? TagName { get;set; }
        public int BookId { get; set; }
        public List<Book>? Books { get; set; }
    }
}
