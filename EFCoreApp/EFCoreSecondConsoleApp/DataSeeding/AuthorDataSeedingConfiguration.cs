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
    public class AuthorDataSeedingConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(
                 new Author
                 {
                     AuthorId = 1,
                      Name = "Martin Fowler",
                       WebUrl = "http://ma"
                 },
                   new Author
                   {
                       AuthorId = 2,
                       Name = "Eric Evans",
                       WebUrl = "http://evenyour"
                   },
                     new Author
                     {
                         AuthorId =3,
                         Name = "Future Person",
                         WebUrl = "http://futer"
                     }

                );
        }
    }
}
