using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreConsoleApp.Models
{
    public class BookAuthor
    {
        public int BookAuthorId { get;set; }
        public int BookId { get; set; }
        public int Order { get; set; }
        public List<Book> Books { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
    }
}
