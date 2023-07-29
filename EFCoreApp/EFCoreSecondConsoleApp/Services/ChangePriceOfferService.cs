using EFCoreSecondConsoleApp.Data;
using EFCoreSecondConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Services
{
    public class ChangePriceOfferService : IChangePriceOfferService
    {
        public ApplicationDbContext _context { get; set; }
        public Book OrgBook { get; private set; }

        public ChangePriceOfferService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Book AddUpdatePriceOffer(PriceOffer promotion)
        {
            throw new NotImplementedException();
        }

        public PriceOffer GetOriginal(int id)
        {
            OrgBook = _context.Books
                .Include(r => r.Promotion)
                .Single(k => k.BookId == id);

            //return OrgBook?.Promotion ?? new PriceOffer
            //{
            //    BookId = id,
            //    NewPrice = OrgBook.Price
            //};

            return new PriceOffer() { };
        }
    }
}
