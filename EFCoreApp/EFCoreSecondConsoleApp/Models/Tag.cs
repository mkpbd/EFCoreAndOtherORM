using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Models
{
    public class Tag
    {
        public int TagId { get;set; }
        public string TagName { get;set; }
        public string TagValue { get;set; }
        public int BookId { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
