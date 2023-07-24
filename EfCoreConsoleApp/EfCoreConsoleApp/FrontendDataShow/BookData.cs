﻿using EfCoreConsoleApp.Data;
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

                    Console.WriteLine($"{book.Title} by {book.Author.Name}");

                    Console.WriteLine(" " + "Published on " +
                    $"{book.PublishedOn:dd-MMM-yyyy}" +
                    $". {webUrl}");
                }
            }

        }

        public static void ChangeWebUrl()
        {
            Console.Write("New Quantum Networking WebUrl > ");
            var newWebUrl = Console.ReadLine();
            using (var db = new ApplicationDbContext())
            {
                try
                {
                    var singleBook = db.Books
                    .Include(book => book.Author)
                    .Single(book => book.Title == "Quantum Networking");

                    singleBook.Author.WebUrl = newWebUrl;
                    db.SaveChanges();
                    Console.WriteLine("... SavedChanges called.");
                }
                catch (Exception ex)
                {

                }
            }


            BookData.ListAllBook();
        }


        public static void EagerLoadingOfBookClass()
        {
            using (var db = new ApplicationDbContext())
            {
                var firstBook = db.Books
                .Include(book => book.AuthorsLink)
                .ThenInclude(bookAuthor => bookAuthor.Authors)
                .Include(book => book.Reviews)
                .Include(book => book.Tags)
                .Include(book => book.Promotion)
                .FirstOrDefault();
            }
        }

        public static void EagerLoadingOfBooksFilteringSorting()
        {
            using (var db = new ApplicationDbContext())
            {
                var firstBook = db.Books
                .Include(book => book.AuthorsLink
                .OrderBy(bookAuthor => bookAuthor.Order))
                .ThenInclude(bookAuthor => bookAuthor.Authors)
                .Include(book => book.Reviews
                .Where(review => review.NumStars == 5))
                .Include(book => book.Promotion)
                .First();
            }
        }

        public static void ExplicitLoadingOfTheBookClass()
        {
            using (var context = new ApplicationDbContext())
            {
                var firstBook = context.Books.First();
                context.Entry(firstBook)
                .Collection(book => book.AuthorsLink).Load();
                foreach (var authorLink in firstBook.AuthorsLink)
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

        public static void ExplicitLoadingOfTheBookClassWithRefinedSetOfRelatedData()
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
    }
}
