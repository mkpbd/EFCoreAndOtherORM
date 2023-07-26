using EFCoreSecondConsoleApp.CommonViewModel;
using EFCoreSecondConsoleApp.DTO;
using EFCoreSecondConsoleApp.Enums;
using EFCoreSecondConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Common
{
    public class SortFilterPageOptions
    {
        public OrderByOptions OrderByOptions { get; set; }
        public int PageSize { get; set; } = 0;
        public int PageNum { get; set; } = 0;
        public BooksFilterBy FilterBy { get; set; }

        public string? FilterValue{get; set;}

        internal void SetupRestOfDto(IQueryable<BookListDto> booksQuery)
        {
            throw new NotImplementedException();
        }
    }
}
