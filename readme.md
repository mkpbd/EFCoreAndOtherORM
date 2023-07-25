Intall From Nuget Package

Microsoft.EntityFrameworkCore

Microsoft.EntityFrameworkCore.Design

Microsoft.EntityFrameworkCore.Tools

Microsoft.EntityFrameworkCore.SqlServer

**Overview of EF Core**

You can use EF Core as an O/RM that maps between the relational database and the .NET world of classes and software code.

| Relational database                 | .NET software                                    |
| ----------------------------------- | ------------------------------------------------ |
| Table                               | .NET class                                       |
| Table columns                       | Class properties/fields                          |
| Rows                                | Elements in .NET collections—for instance, List |
| Primary keys: unique row            | A unique class instance                          |
| Foreign keys: define a relationship | Reference to another class                       |
| SQL—for instance, WHERE            | .NET LINQ—for instance, Where(p => …           |

Tables In Database

1. A Books table holding the book information
2. An Author table holding the author of each book

![1690244894653](image/readme/1690244894653.png)

**Relational database with two tables: Books and Author**

The Books table name comes from the ****DbSet `<Book>` Books**** property.

 The Authors table name comes from the DbSet `<Author>` Author property.

### The classes that map to the database: Book and Author

EF Core maps classes to database tables. Therefore, you need to create a class that will define the database table or match a database table if you already have a database. Lots of rules and configurations exist

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSecondConsoleApp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}

```

![1690251789616](image/readme/1690251789616.png)

The.NET class Book, on the left, maps to a database table called Books, on the right. This is a typical way to build your application, with multiple classes that map to database tables.

This class has the same structure as the Book class with a primary key that follows the EF Core naming conventions of `<ClassName>`Id  example Book as primary key **BookId**
