using EFCoreSecondConsoleApp.CommonViewModel;
using EFCoreSecondConsoleApp.DTO;
using EFCoreSecondConsoleApp.Enums;
using EFCoreSecondConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp
{
    public static class BookMapExtension
    {
        public static IQueryable<BookListDto> MapBookToDto(this IQueryable<Book> books)
        {
            return books.Select(book => new BookListDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Price = book.Price,
                PublishedOn = book.PublishedOn,

                ActualPrice = book.Promotion == null
                ? book.Price
                : book.Promotion.NewPrice,

                PromotionPromotionalText = book.Promotion == null ? null
                : book.Promotion.PromotionalText,

                AuthorsOrdered = string.Join(", ",
                book.bookAuthors.OrderBy(ba => ba.Order).Select(ba => ba.Author.Name)),

                ReviewsCount = book.Reviews.Count,
                ReviewsAverageVotes = book.Reviews.Select(review => (double?)review.NumStars).Average(),

                TagStrings = book.Tags.Select(x => x.TagId.ToString()).ToArray(),
            });
        }

        public static IQueryable<BookListDto> OrderBooksBy(this IQueryable<BookListDto> books, OrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case OrderByOptions.SimpleOrder:
                    return books.OrderByDescending(
                    x => x.BookId);
                case OrderByOptions.ByVotes:
                    return books.OrderByDescending(x =>
                    x.ReviewsAverageVotes);
                case OrderByOptions.ByPublicationDate:
                    return books.OrderByDescending(
                    x => x.PublishedOn);
                case OrderByOptions.ByPriceLowestFirst:
                    return books.OrderBy(x => x.ActualPrice);
                case OrderByOptions.ByPriceHighestFirst:
                    return books.OrderByDescending(
                    x => x.ActualPrice);
                default:
                    throw new ArgumentOutOfRangeException(
                    nameof(orderByOptions), orderByOptions, null);
            }
        }


        public static IQueryable<BookListDto> FilterBooksBy(this IQueryable<BookListDto> books,
                        BooksFilterBy filterBy, string filterValue)
        {
            if (string.IsNullOrEmpty(filterValue))
                return books;
            switch (filterBy)
            {
                case BooksFilterBy.NoFilter:
                    return books;
                case BooksFilterBy.ByVotes:
                    var filterVote = int.Parse(filterValue);
                    return books.Where(x => x.ReviewsAverageVotes > filterVote);
                case BooksFilterBy.ByTags:
                    return books.Where(x => x.TagStrings.Any(y => y == filterValue));
                case BooksFilterBy.ByPublicationYear:
                    if (filterValue == "AllBooksNotPublishedString")
                        return books.Where(x => x.PublishedOn > DateTime.UtcNow);
                    var filterYear = int.Parse(filterValue);
                    return books.Where(
                    x => x.PublishedOn.Year == filterYear
                    && x.PublishedOn <= DateTime.UtcNow);
                default:
                    throw new ArgumentOutOfRangeException
                    (nameof(filterBy), filterBy, null);
            }
        }


        public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageNumZeroStart, int pageSize)
        {
            if (pageSize == 0)
                throw new ArgumentOutOfRangeException
                (nameof(pageSize), "pageSize cannot be zero.");
            if (pageNumZeroStart != 0)
                query = query
                .Skip(pageNumZeroStart * pageSize);
            return query.Take(pageSize);
        }


    }
}
