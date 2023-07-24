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
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        // RelatinsShip start here

        // One to one relation Shipe
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        // Many to  Many RelationShip
        public int TagId { get; set; }
        public List<Tag>? Tags { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public PriceOffer Promotion { get; set; }
        public ICollection<BookAuthor>? AuthorsLink { get; set; }
    }
}
