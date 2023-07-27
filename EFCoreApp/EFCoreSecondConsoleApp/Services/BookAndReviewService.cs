using EFCoreSecondConsoleApp.Data;
using EFCoreSecondConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Services
{
    public class BookAndReviewService
    {
        private readonly ApplicationDbContext _context;
        public BookAndReviewService()
        {

            _context = new ApplicationDbContext();
        }
        public void AddBookWithReviews()
        {
            var book = new Book
            {
                Title = "New Book Added",
                Description = "This is a description Two who we are",
                Price = 100,


                Reviews = new List<Review> { new Review { NumStars = 5, Comment = "this is comment", VoterName = "kamal passa" } },
                Tags = new List<Tag> { new Tag {
                     TagName = "abc tag",
                      TagValue = "we have a gat value",
                } },

                Author = new Author { Name = "kamal ", WebUrl = "http://abc.com" },


            };

            try
            {
                _context.Add(book);
                _context.SaveChanges();

                Console.WriteLine("data has been saved");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Data has not been save");
            }

        }



        public void AddExistingAuthorBook()
        {
            var foundAuthor = _context.Author.SingleOrDefault(author => author.Name == "Eric Evans");
            if (foundAuthor == null) throw new Exception("Author not found");
            var book = new Book
            {
                Title = "Test Book",
                PublishedOn = DateTime.Today,
                AuthorId = foundAuthor.AuthorId,
            };
            book.bookAuthors = new List<BookAuthor>
                {
                    new BookAuthor
                    {
                        Books = new List<Book>{ book },
                        Author = foundAuthor
                    }
                };

            try
            {
                _context.Add(book);
                _context.SaveChanges();

                Console.WriteLine("Sava data ");

            }
            catch (Exception ex)
            {
                Console.WriteLine("data not save");
            }
        }

        public void UpdateBookOnExsitingData()
        {
            try
            {
                var book = _context.Books
    .SingleOrDefault(p => p.Title == "Quantum Networking");

                if (book == null)   throw new Exception("Book not found");

                book.PublishedOn = new DateTime(2058, 1, 1);
                book.Price = 200;
                _context.SaveChanges();
                Console.WriteLine("Update data");
            }
            catch (Exception ex)
            {
                Console.WriteLine("dose not update data");
            }
        }
    }

}
