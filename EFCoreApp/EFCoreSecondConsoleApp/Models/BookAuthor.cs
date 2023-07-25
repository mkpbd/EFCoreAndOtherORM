using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Models
{
    public class BookAuthor

    {
        public int BookAuthorId { get; set; }
        public int BookId { get; set; }
        public ICollection<Book> Books { get; set; }
        public int AuthorId { get;  set; }
        public ICollection<Author> Authors { get; set; }
    }
}
