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

The Book class also has a navigational property of type Author and an int type property called AuthorId that matches
the Author's primary key. These two properties tell EF Core that you want a link from the Book class to the Author class and that the AuthorId property should be used as the foreign key to link the two tables in the database.

**The Author class**

```csharp
namespace EFCoreSecondConsoleApp.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string WebUrl { get; set; }

    }
}
```

![1690252228685](image/readme/1690252228685.png)

#### The application's DbContext

The other important part of the application is DbContext, a class you create that inherits from EF Core’s DbContext class. This class holds the information EF Core needs to configure that database mapping and is also the class you use in your code to
access the database

**You must have a class that inherits from the EF Core class DbContext. This class holds the information and configuration for accessing your database.**

```csharp
public class ApplicationDbContext : DbContext
{
private const string ConnectionString =
@"Server=(localdb)\mssqllocaldb;
Database=MyFirstEfCoreDb;
Trusted_Connection=True”;
protected override void OnConfiguring(
DbContextOptionsBuilder optionsBuilder)
{
optionsBuilder
.UseSqlServer(connectionString);
}
public DbSet<Book> Books { get; set; }
```

![1690252418535](image/readme/1690252418535.png)

#### Modeling the database

Before you can do anything with the database, EF Core must go through a process that I refer to as modeling the database

This modeling is EF Core’s way of working out what the database looks like by looking at the classes and other EF Core configuration data. Then EF Core uses the resulting model in all database accesses. The modeling process is kicked off the first time you create the application's **DbContext**, in this case called **ApplicationDbContext**. It has one property, **DbSet** `<Book>`, which is the way that the code accesses the database

![1690253745733](image/readme/1690253745733.png)

EF Core will create a model of the database your classes map to. First, it looks at the classes you have defined via the DbSet `<T>` properties; then it looks down all the references to other classes. Using these classes, EF Core can work out the default model of the database. But then it runs the OnModelCreating method in the application's DbContext, which you can override to add your specific commands to configure the database the way you want it.

the modeling steps that EF Core uses on our AppDbContext, which happens the first time you create an instance of the AppDbContext.

#### Reading data from the database

Reads All Books and Authors

```csharp
 using (var db = new ApplicationDbContext())
            {
                foreach (var book in
                db.Books.AsNoTracking()
                .Include(book => book.Author))
                {
                    var webUrl = book.Author.WebUrl == null
                    ? "- no web URL given -"
                    : book.Author.WebUrl;
                    Console.WriteLine(
                    $"{book.Title} by {book.Author.Name}");
                    Console.WriteLine(" " +
                    "Published on " +
                    $"{book.PublishedOn:dd-MMM-yyyy}" +
                    $". {webUrl}");
                }
            }
```

![1690269123895](image/readme/1690269123895.png)

EF Core uses Microsoft’s .NET’s Language Integrated Query (LINQ) to carry the commands it wants done, and normal .NET classes to hold the data.

The query **db.Books.AsNoTracking().Include(book => book.Author)** accesses the *DbSet `<Book>`* property in the application's **DbContext** and adds a .Include
**(book => book.Author)** at the end to ask that the Author parts of the relationship are loaded too. This is converted by the database provider into an SQL command to access the database. The resulting SQL is cached to avoid the cost of retranslation if the same database access is used again

![1690269695510](image/readme/1690269695510.png)

**SQL command produced to read Books and Author**

```csharp
SELECT [b].[BookId],
[b].[AuthorId],
[b].[Description],
[b].[PublishedOn],
[b].[Title],
[a].[AuthorId],
[a].[Name],
[a].[WebUrl]
FROM [Books] AS [b]
INNER JOIN [Author] AS [a] ON
[b].[AuthorId] = [a].[AuthorId]
```

**The code to update the author's WebUrl of the book Quantum Networking**

```csharp
 public static void ChangeWebUrl()
        {
            Console.Write("New Quantum Networking WebUrl > ");
            var newWebUrl = Console.ReadLine();
            using (var db = new ApplicationDbContext())
            {
                var singleBook = db.Books
                .Include(book => book.Author)
                .Single(book => book.Title == "Quantum Networking");
                singleBook.Author.WebUrl = newWebUrl;
                db.SaveChanges();
                Console.WriteLine("... SavedChanges called.");
            }
            ListAll();
        }
```

![1690270519116](image/readme/1690270519116.png)

#### The execute command

EF Core can translate an expression tree into the correct commands for the database you’re using. In EF Core, a query is executed against the database when

1. It's enumerated by a foreach statement.
2. It's enumerated by a collection operation such as **ToArray, ToDictionary, ToList, ToListAsync**, and so forth.
3. LINQ operators such as First or Any are specified in the outermost part of the query

You'll use certain EF Core commands, such as Load, in the explicit loading of a relationship later in this chapter.

#### The two types of database queries

The other type of query is an **AsNoTracking** query, also known as a **read-only query**. This query has the EF Core’s **AsNoTracking** method added to the LINQ query.

As well as making the query **read-only**, the **AsNoTracking** method improves the performance of the query by turning off certain EF Core features.

```csharp
context.Books.AsNoTracking().Where(p => p.Title.StartsWith("Quantum")).ToList();
```

provides a detailed list of the differences between the normal, **read-write** query and the **AsNoTracking**, read-only query.

#### Eager loading: Loading relationships with the primary entity class

The first approach to loading related data is eager loading, which entails telling EF Core to load the relationship in the same query that loads the primary entity class. Eager loading is specified via two fluent methods, **Include and ThenInclude**. The next listing shows the loading of the first row of the Books table as an instance of the Book entity class and the eager loading of the single relationship, Reviews.

```csharp
var firstBook = context.Books
.Include(book => book.Reviews)
.FirstOrDefault();
```

![1690294196205](image/readme/1690294196205.png)

```csharp

SELECT "t"."BookId", "t"."Description", "t"."ImageUrl",
"t"."Price", "t"."PublishedOn", "t"."Publisher",
"t"."Title", "r"."ReviewId", "r"."BookId",
"r"."Comment", "r"."NumStars", "r"."VoterName"
FROM (
SELECT "b"."BookId", "b"."Description", "b"."ImageUrl",
"b"."Price", "b"."PublishedOn", "b"."Publisher", "b"."Title"
FROM "Books" AS "b"
LIMIT 1
) AS "t"
LEFT JOIN "Review" AS "r" ON "t"."BookId" = "r"."BookId"
ORDER BY "t"."BookId", "r"."ReviewId"
```
