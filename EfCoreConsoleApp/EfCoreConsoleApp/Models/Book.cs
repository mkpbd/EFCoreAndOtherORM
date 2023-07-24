using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreConsoleApp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }

        // RelatinsShip start here

        // One to one relation Shipe
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        // Many to  Many RelationShip
        public int TagId { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
