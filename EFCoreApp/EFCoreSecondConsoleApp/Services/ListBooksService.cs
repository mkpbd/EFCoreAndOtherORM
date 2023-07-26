using EFCoreSecondConsoleApp.Common;
using EFCoreSecondConsoleApp.Data;
using EFCoreSecondConsoleApp.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Services
{
    public class ListBooksService
    {
        private readonly ApplicationDbContext _context;
        public ListBooksService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<BookListDto> SortFilterPage
        (SortFilterPageOptions options)
        {
            var booksQuery = _context.Books
            .AsNoTracking()
            .MapBookToDto()
            .OrderBooksBy(options.OrderByOptions)
            .FilterBooksBy(options.FilterBy,  options.FilterValue);
            options.SetupRestOfDto(booksQuery);
            return booksQuery.Page(options.PageNum - 1,
            options.PageSize);
        }
    }
}
