using EfCoreConsoleApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreConsoleApp.FrontendDataShow
{
    public class BookData
    {
        public static void ListAllBook()
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

                    Console.WriteLine(  $"{book.Title} by {book.Author.Name}");

                    Console.WriteLine(" " + "Published on " +
                    $"{book.PublishedOn:dd-MMM-yyyy}" +
                    $". {webUrl}");
                }
            }

        }
    }
}
