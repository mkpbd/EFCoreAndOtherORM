using EFCoreSecondConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.DataSeeding
{
    public class ConfigurationBookData : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                    new Book
                    {
                        AuthorId = 1,
                        BookId = 1,
                        Description = "Improving h",
                        Title = "Refactoring",
                        PublishedOn = new DateTime(1990, 7, 08)

                    }
                    ,
                    new Book
                    {
                        AuthorId = 1,
                        BookId = 2,
                        Description = "Written in d",
                        Title = "Patterns of Enterprise Ap",
                        PublishedOn = new DateTime(2002, 10, 15)

                    },
                     new Book
                     {
                         AuthorId = 2,
                         BookId = 3,
                         Description = "Linking bus",
                         Title = "Domain-Driven Design",
                         PublishedOn = new DateTime(2003, 8, 30)

                     },
                       new Book
                       {
                           AuthorId = 3,
                           BookId = 4,
                           Description = "Entanged q",
                           Title = "Quantum Networking",
                           PublishedOn = new DateTime(2003, 8, 30)

                       }
                );
        }
    }
}
