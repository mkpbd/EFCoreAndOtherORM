using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }


        public ICollection<Review> Reviews { get; set; }

        public ICollection<Tag> Tags { get; set; }
        public ICollection<Promotion> Promotion { get; set; }

        public List<BookAuthor> bookAuthors { get; set; }
    }
}
