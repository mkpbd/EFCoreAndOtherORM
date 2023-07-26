using EFCoreSecondConsoleApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.DataForFrontend
{
    public class BookAndAutherData
    {
        public static void ListAll()
        {
            using (var db = new ApplicationDbContext())
            {
                foreach (var book in
                db.Books.AsNoTracking()
                .Include(book => book.Author))
                {
                    var webUrl = book.Author.WebUrl == null
                    ? "- no web URL given -"
                    : book.Author.WebUrl;
                    Console.WriteLine(
                    $"{book.Title} by {book.Author.Name}");
                    Console.WriteLine(" " +
                    "Published on " +
                    $"{book.PublishedOn:dd-MMM-yyyy}" +
                    $". {webUrl}");
                }
            }
        }

        // Updating Data base special condition
        public static void ChangeWebUrl()
        {
            Console.Write("New Quantum Networking WebUrl > ");
            var newWebUrl = Console.ReadLine();
            using (var db = new ApplicationDbContext())
            {
                var singleBook = db.Books
                .Include(book => book.Author)
                .Single(book => book.Title == "Quantum Networking");
                singleBook.Author.WebUrl = newWebUrl;
                db.SaveChanges();
                Console.WriteLine("... SavedChanges called.");
            }
            ListAll();
        }

        public static void TheTwoTypesOfDatabaseQuerys()
        {
            using (var context = new ApplicationDbContext())
            {
                context.Books.AsNoTracking().Where(p => p.Title.StartsWith("Quantum")).ToList();
            }
        }


        public static void EagerLoadingOfFirstBookWithCorrespondingReviews()
        {
            using (var context = new ApplicationDbContext())
            {
                var firstBook = context.Books
                .Include(book => book.Reviews)
                .FirstOrDefault();
            }
        }


        public static void EagerLoadingOfTheBookClassAndAllTheRelatedData()
        {
            using (var context = new ApplicationDbContext())
            {
                var firstBook = context.Books
                //.Include(book => book.AuthorsLink)
                //.ThenInclude(bookAuthor => bookAuthor.Author)
                .Include(bookAuthor => bookAuthor.Author)
                .Include(book => book.Reviews)
                .Include(book => book.Tags)
                .Include(book => book.Promotion)
                .FirstOrDefault();
            }

        }

        public static void SortingAndFilteringWhenUsingIncludeOrThenInclude()
        {
            using (var context = new ApplicationDbContext())
            {
                var firstBook = context.Books
            .Include(book => book.bookAuthors
            .OrderBy(bookAuthor => bookAuthor.Order))
            .ThenInclude(bookAuthor => bookAuthor.Authors)
            .Include(book => book.Reviews
            .Where(review => review.NumStars == 5))
            .Include(book => book.Promotion)
            .First();
            }
        }


        public static void ExplicitLoadingOfTheBookClassAndRelatedData()
        {
            using (var context = new ApplicationDbContext())
            {
                var firstBook = context.Books.First();
                context.Entry(firstBook)
                .Collection(book => book.bookAuthors).Load();
                foreach (var authorLink in firstBook.bookAuthors)
                {
                    context.Entry(authorLink)
                    .Reference(bookAuthor =>
                    bookAuthor.Authors).Load();
                }

                context.Entry(firstBook)
                .Collection(book => book.Tags).Load();
                context.Entry(firstBook)
                .Reference(book => book.Promotion).Load();
            }
        }
        public static void ExplicitingLoadingOfBookClassWithRedinedSetOfRelatedData()
        {
            using (var context = new ApplicationDbContext())
            {
                var firstBook = context.Books.First();
                var numReviews = context.Entry(firstBook)
                .Collection(book => book.Reviews)
                .Query().Count();

                var starRatings = context.Entry(firstBook)
                .Collection(book => book.Reviews)
                .Query().Select(review => review.NumStars)
                .ToList();
            }
        }

        public static void SelectOfTheBookClassPickingSpecificProperticsAndOneCalculation()
        {
            using (var context = new ApplicationDbContext())
            {
                var books = context.Books
                .Select(book => new
                {
                    book.Title,
                    book.Price,
                    NumReviews
                = book.Reviews.Count,
                }
                ).ToList();

            }
        }

        public static void SelectQueryThatIncludesANonSQLCommandStringJoin()
        {
            using (var context = new ApplicationDbContext())
            {
                var firstBook = context.Books
                .Select(book => new
                {
                    book.BookId,
                    book.Title,
                    AuthorsString = string.Join(", ",
                book.bookAuthors
                .OrderBy(ba => ba.Order)
                .Select(ba => ba.Author.Name))
                }
                ).First();

            }
        }

        public static void StringCommandSQLServerDatabase()
        {
            using (var context = new ApplicationDbContext())
            {
                var books = context.Books
                    .Where(p => p.Title.StartsWith("The"))
                    .ToList();

                var books1 = context.Books
                .Where(p => p.Title.EndsWith("MAT."))
                .ToList();

                var books2 = context.Books
                .Where(p => p.Title.Contains("cat"));

               

            }
        }
    }
}
