using EFCoreSecondConsoleApp.Contracts;
using EFCoreSecondConsoleApp.Data;
using EFCoreSecondConsoleApp.DTO;
using EFCoreSecondConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Services
{
    public class ChangePubDateService : IChangePubDateService
    {
        private readonly ApplicationDbContext _context;

        string json;
        public ChangePubDateService()
        {
            _context = new ApplicationDbContext();
        }

        public ChangePubDateDto GetOriginal(int id)
        {
            return _context.Books.Select(p => new ChangePubDateDto
            {
                BookId = p.BookId,
                Title = p.Title,
                PublishedOn = p.PublishedOn
            })
             .Single(k => k.BookId == id);
        }

        public Book UpdateBook(ChangePubDateDto dto)
        {
            var book = _context.Books.SingleOrDefault(x => x.BookId == dto.BookId);
            if (book == null) throw new ArgumentException("Book not found");
            book.PublishedOn = dto.PublishedOn;
            _context.SaveChanges();
            return book;
        }

        public void GetDataFromDataBase()
        {
            var author = _context.Books
                .Where(p => p.Title == "Quantum Networking")
                .Select(p => p.bookAuthors.First().Author)
                .Single();
            author.Name = "Future Person 2";
            json = JsonConvert.SerializeObject(author);
        }

        public void DeserialiseDataOfJsonFormat()
        {
            var author = JsonConvert.DeserializeObject<Author>(json);
            _context.Author.Update(author);
            _context.SaveChanges();
        }

        public void AddingNewPromosionPriceToExistingBook()
        {
            var book = _context.Books.Include(p => p.Promotion).First(p => p.Promotion == null);
            book.Promotion = new Promotion
            {
                NewPrice = book.Price / 2,
                PromotionalText = "Half price today!"
            };
            _context.SaveChanges();
        }

        public void TheBookEntityClassShowingTheRelationshipsToUpdate()
        {

        }
    }
}
