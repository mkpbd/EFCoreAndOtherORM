The NuGet packages are listed for the activity project using the Manage NuGet Packages for Solution option

1. Microsoft.EntityFrameworkCore
2. Microsoft.EntityFrameworkCore.Design
3. Microsoft.EntityFrameworkCore.SqlServer
4. Microsoft.EntityFrameworkCore.Tools

 add-migration Initial_Migration

You can use EF Core as an O/RM that maps between the relational database and the
.NET world of classes and software code.

***EF Core mapping between a database and .NET software***

| Relational Database                 | .NET Software                                      |
| ----------------------------------- | -------------------------------------------------- |
| Table                               | .NET class                                         |
| Table Columns                       | Class Properties/Fields                            |
| Rows                                | Elements in .NET Collections  for instance , List |
| Primay keys: unique Row             | A unique class instance                            |
| Foregin keys: define a relationship | Reference to antoher class                         |
| SQL for instance, WHERE             | .NET LINQ for instance, Where (p => ....)          |

For this MyFirstEfCoreApp application example, I created a simple database, shown in figure  with only two tables:

1. A Books table holding the book information
2. An Author table holding the author of each book

![1690034180926](image/readme/1690034180926.png)

ADVANCED NOTE In this example, I let EF Core name the tables using its default configuration settings. The Books table name comes from the DbSet `<Book>`

Books property  . The Author table name hasn’t got a DbSet `<T>` property, so EF Core uses the name of the class.

![1690034327777](image/readme/1690034327777.png)

### The classes that map to the database: Book and Author

EF Core maps classes to database tables. Therefore, you need to create a class that will define the database table or match a database table if you already have a database

![1690034449548](image/readme/1690034449548.png)

The Author property is an EF Core navigational property. EF Core uses this on a save to see whether the Book has an Author class attached. If so, it sets the foreign key, AuthorId. Upon loading a Book class, the method Include will fill this property with the Author class that's linked to this Book class by using the foreign key, AuthorId.

I ll be using: Author. This class has the same structure as the Book class  with a primary key that follows the EF Core naming
conventions of `<ClassName>`Id . The Book class also has a navigational property of type Author and an int type property called AuthorId that matches the Author’s primary key. These two properties tell EF Core that you want a link from  the Book class to the Author class and that the AuthorId property should be used as the foreign key to link the two tables in the database.

![1690035736436](image/readme/1690035736436.png)

### The application's DbContext

The other important part of the application is DbContext, a class you create that inherits from EF Core’s DbContext class. This class holds the information EF Core needs to configure that database mapping and is also the class you use in your code to
access the database

![1690035940014](image/readme/1690035940014.png)

### Modeling the database

Before you can do anything with the database, EF Core must go through a process that I refer to as modeling the database. This modeling is EF Core’s way of working out what the database looks like by looking at the classes and other EF Core configuration data. Then EF Core uses the resulting model in all database accesses.

The modeling process is kicked off the first time you create the application's DbContext, in this case called AppDbContext . It has one property, DbSet `<Book>`, which is the way that the code accesses the database.

![1690036594168](image/readme/1690036594168.png)

the modeling steps that EF Core uses on our AppDbContext, which happens the first time you create an instance of the AppDbContext. (After that, the model is cached, so that subsequent instances are created quickly.) The following text
provides a more detailed description of the process:

1. EF Core looks at the application’s DbContext and finds all the public DbSet `<T>` properties. From this data, it defines the initial name for the one table it finds: Books.
2. EF Core looks through all the classes referred to in DbSet `<T>` and looks at its properties to work out the column names, types, and so forth. It also looks for special attributes on the class and/or properties that provide extra modeling
   information.
3. EF Core looks for any classes that the DbSet `<T>` classes refer to. In our case, the Book class has a reference to the Author class, so EF Core scans that class too. I carries out the same search on the properties of the Author class as it did on the
   Book class in step 2. It also takes the class name, Author, as the table name.
4. For the last input to the modeling process, EF Core runs the virtual method **OnModelCreating** inside the application’s **DbContext**. In this simple application, you don’t override the OnModelCreating method, but if you did, you
   could provide extra information via a fluent API to do more configuration of the modeling.

### **Reading data from the database**

![1690038676618](image/readme/1690038676618.png)

EF Core uses Microsoft's .NET’s Language Integrated Query (LINQ) to carry the commands it wants done, and normal .NET classes to hold the data.

The query db.Books.AsNoTracking().Include(book => book.Author) accesses the DbSet `<Book>` property in the application’s DbContext and adds a ***.Include(book => book.Author)*** at the end to ask that the Author parts of the relationship are loaded too. This is converted by the database provider into an SQL command to access the database. The resulting SQL is cached to avoid the cost of retranslation if the same database access is used again.

![1690039565366](image/readme/1690039565366.png)

![1690040277898](image/readme/1690040277898.png)

![1690040350772](image/readme/1690040350772.png)

RelationShips

1. One-to-one relationship—PriceOffer to a Book
2. One-to-many relationship—Book with Reviews
3. Many-to-many relationship—Books linked to Authors and Books linked to Tags

ONE-TO-ONE RELATIONSHIP: PRICEOFFER TO A BOOK

A book can have a promotional price applied to it with an optional row in the Price- Offer, which is an example of a one-to-one relationship.

Technically, the relationship is one-to-zero-or-one, but EF Core handles it the same way

![1690069505748](image/readme/1690069505748.png)

I have a primary key and a foreign key to make the relationship easier to understand. But for one-to-one relationships,
you can make the foreign key be the primary key too. In the PriceOffer table , you would have a primary key, called BookId, which would also be the foreign key. As a result, you lose the PriceOfferId column, which makes the table slightly more efficient from the database side

**ONE-TO-MANY RELATIONSHIP: REVIEWS TO A BOOK**

You want to allow customers to review a book; they can give a book a star rating and optionally leave a comment. Because a book may have no reviews or many (unlimited) reviews, you need to create a table to hold that data.

![1690070118676](image/readme/1690070118676.png)

In the Summary display, you need to count the number of reviews and work out the average star rating to show a summary. Here's a typical onscreen display you might produce from this one-to-many relationship

**MANY-TO-MANY RELATIONSHIP: MANUALLY CONFIGURED**

Books can be written by one or more authors, and an author may write one or more books. Therefore, you need a table called Books to hold the books data and another table called Authors to hold the authors. The link between the Books and Authors
tables is called a many-to-many relationship, which in this case needs a linking table to achieve this relationship.
In this case, you create your own linking table with an Order value in it because the names of the authors in a book must be displayed in a specific order

![1690070489201](image/readme/1690070489201.png)

This table uses the foreign keys as the primary keys. Because primary keys must be unique, this ensures that only one link can exist between a book and an author.

*The three tables involved in creating the many-to-many relationship between the Books table and the Authors table. I use a many-to-many relationship because books can have many authors, and authors may have written many books. The extra feature needed here is the Order value, because the order in which authors are listed in a book matters, so I use the Order value to display the authors in the correct sequence.*

### Understanding database queries

EF Core database query, with the three main parts of the query highlighted.

```csharp
context.Books.Where(p => p.Title.StartsWith("Quantum").ToList();
```

![1690117443741](image/readme/1690117443741.png)

### The execute command

The last part of the command reveals something about LINQ. Until a final execute command is applied at the end of the sequence of LINQ commands, the LINQ is held as a series of commands in what is called an expression tree.

which means that it hasn’t been executed on the data yet. EF Core can translate an expression tree into the correct commands for the database you’re using. In EF Core, a query is executed against the database when

1. It's enumerated by a foreach statement.
2. It’s enumerated by a collection operation such as ToArray, ToDictionary,
3. ToList, ToListAsync, and so forth.
4. LINQ operators such as First or Any are specified in the outermost part of the query.

   You’ll use certain EF Core commands, such as Load, in the explicit loading of a relationship later in this chapter.

   At this point, your LINQ query will be converted to database commands and sent to the database. If you want to build high-performance database queries, you want all your LINQ commands for filtering, sorting, paging, and so on to come before you call an execute command.

```csharp
context.Books.AsNoTracking().Where(p => p.Title.StartsWith("Quantum")).ToList();
```

A detailed list of the differences between the normal, read-write query and the AsNoTracking, read-only query.

#### Eager loading: Loading relationships with the primary entity class

Eager loading is specified via two fluent methods, ***Include***and ***ThenInclude***. The next listing shows the loading of the first row of the **Books** table as an instance of the **Book** entity class and the eager loading of the single relationship, **Reviews**

```csharp
var firstBook = context.Books.Include(book => book.Reviews).FirstOrDefault();
```

![1690118690795](image/readme/1690118690795.png)

Now let’s look at a more complex example. The following listing shows a query to get the first Book, with eager loading of all its relationships—in this case, AuthorsLink and the second-level Author table, the Reviews, and the optional Promotion class

```csharp
var firstBook = context.Books
.Include(book => book.AuthorsLink
.OrderBy(bookAuthor => bookAuthor.Order))
.ThenInclude(bookAuthor => bookAuthor.Author)
.Include(book => book.Reviews
.Where(review => review.NumStars == 5))
.Include(book => book.Promotion)
.First
```

![1690119603954](image/readme/1690119603954.png)

```csharp
var firstBook = context.Books.First();
context.Entry(firstBook)
.Collection(book => book.AuthorsLink).Load();
foreach (var authorLink in firstBook.AuthorsLink)
{
context.Entry(authorLink)
.Reference(bookAuthor =>
bookAuthor.Author).Load();
}
context.Entry(firstBook)
.Collection(book => book.Tags).Load();
context.Entry(firstBook)
.Reference(book => book.Promotion).Load();
```

![1690120251206](image/readme/1690120251206.png)

The second approach to loading data is explicit loading. After you’ve loaded the primary entity class, you can explicitly load any other relationships you want explicit loading .First, it loads the Book; then it uses explicit-loading commands to read all the relationships.

![1690120410699](image/readme/1690120410699.png)

Alternatively, explicit loading can be used to apply a query to the relationship instead of loading the relationship.  shows the use of the explicit-loading method *Query* to obtain the count of reviews and to load the star ratings of each review.

You can use any standard LINQ command after the Query method, such as *Where* or *OrderBy*

```csharp
var firstBook = context.Books.First();
var numReviews = context.Entry(firstBook)
.Collection(book => book.Reviews)
.Query().Count();

var starRatings = context.Entry(firstBook)
.Collection(book => book.Reviews)
.Query().Select(review => review.NumStars)
.ToList();
```

### Select loading: Loading specific parts of primary entity class and any relationships

```csharp
var books = context.Books
.Select(book => new
{
book.Title,
book.Price,
NumReviews = book.Reviews.Count,
}).ToList();
```

![1690120752188](image/readme/1690120752188.png)

#### **Lazy loading: Loading relationships as required**

Lazy loading makes writing queries easy, but it has a bad effect on database performance. Lazy loading does require some changes to your DbContext or your entity classes, but after you make those changes, reading is easy; if you access a navigational property that isn’t loaded, EF Core will execute a database query to load that navigational
property. You can set up lazy loading in either of two ways

1. Adding the Microsoft.EntityFrameworkCore.Proxies library when configuring your DbContext
2. Injecting a lazy loading method into the entity class via its constructor

#### Using client vs. server evaluation: Adapting data at the last stage of a query

```csharp
var firstBook = context.Books
.Select(book => new
{
book.BookId,
book.Title,
AuthorsString = string.Join(", ",
book.AuthorsLink
.OrderBy(ba => ba.Order)
.Select(ba => ba.Author.Name))
}
).First();
```

![1690121189261](image/readme/1690121189261.png)

Select query that includes a non-SQL command, string.Join

```csharp
var firstBook = context.Books
.Select(book => new
{
book.BookId,
book.Title,
AuthorsString = string.Join(", ",
book.AuthorsLink
.OrderBy(ba => ba.Order)
.Select(ba => ba.Author.Name))
}).First()
```

![1690122798086](image/readme/1690122798086.png)

Running this code on a book that has two authors, Jack and Jill, would cause AuthorsString to contain Jack, Jill, and the BookId, and Title would be set to the value of the corresponding columns in the Books table.

![1690122898255](image/readme/1690122898255.png)

**Building complex queries**

![1690122980475](image/readme/1690122980475.png)

```csharp
public class BookListDto
{
public int BookId { get; set; }
public string Title { get; set; }
public DateTime PublishedOn { get; set; }
public decimal Price { get; set; }
public decimal
ActualPrice { get; set; }
public string
PromotionPromotionalText { get; set; }
public string AuthorsOrdered { get; set; }
public int ReviewsCount { get; set; }
public double?
ReviewsAverageVotes { get; set; }
public string[] TagStrings { get; set; }
}
```

![1690123054130](image/readme/1690123054130.png)

The Select query to fill BookListDto

```csharp
public static IQueryable<BookListDto>
MapBookToDto(this IQueryable<Book> books)
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
PromotionPromotionalText =
book.Promotion == null
? null
: book.Promotion.PromotionalText,
AuthorsOrdered = string.Join(", ",
book.AuthorsLink
.OrderBy(ba => ba.Order)
.Select(ba => ba.Author.Name)),
ReviewsCount = book.Reviews.Count,
ReviewsAverageVotes =
book.Reviews.Select(review =>
(double?) review.NumStars).Average(),
TagStrings = book.Tags
.Select(x => x.TagId).ToArray(),
});
}
```

![1690123147317](image/readme/1690123147317.png)

```csharp
var result = _db.Books
.Where(x => x.PublishedOn <= DateTime.UtcNow.Date)
.Select(x => x.PublishedOn.Year)
.Distinct()
.OrderByDescending(x => x.PublishedOn)
.Select(x => new DropdownTuple
{
Value = x.ToString(),
Text = x.ToString()
}).ToList();
var comingSoon = _db.Books.
Any(x => x.PublishedOn > DateTime.Today);
if (comingSoon)
result.Insert(0, new DropdownTuple{
Value = BookListDtoFilter.AllBooksNotPublishedString,
Text = BookListDtoFilter.AllBooksNotPublishedString
});
return result;
```

![1690123480220](image/readme/1690123480220.png)

```csharp
public static IQueryable<BookListDto> FilterBooksBy(
this IQueryable<BookListDto> books,
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
return books.Where(x =>
x.ReviewsAverageVotes > filterVote);
case BooksFilterBy.ByTags:
return books.Where(x => x.TagStrings
.Any(y => y == filterValue));
case BooksFilterBy.ByPublicationYear:
if (filterValue == AllBooksNotPublishedString)
return books.Where(
x => x.PublishedOn > DateTime.UtcNow);
var filterYear = int.Parse(filterValue);
return books.Where(
x => x.PublishedOn.Year == filterYear
&& x.PublishedOn <= DateTime.UtcNow);
default:
throw new ArgumentOutOfRangeException
(nameof(filterBy), filterBy, null);
}
}
```

![1690123569425](image/readme/1690123569425.png)

```csharp
var books = context.Books
.Where(p => p.Title.StartsWith("The"))
.ToList();
var books = context.Books
.Where(p => p.Title.EndsWith("MAT."))
.ToList();
var books = context.Books
.Where(p => p.Title.Contains("cat"))
```

![1690123619005](image/readme/1690123619005.png)

but you can also define the collation in a query by using the EF.Functions.Collate method. The following code snippet sets an SQL Server collation, which means that this query will compare the string using the Latin1_General_CS_AS (case-sensitive) collation for this query:

```csharp
context.Books.Where( x =>
EF.Functions.Collate(x.Title, "Latin1_General_CS_AS")
== “HELP” //This does not match “help”
```

```csharp
var books = context.Books
.Where(p => EF.Functions.Like(p.Title, "The ___ sat on the %."))
.ToList();
```

Paging the books in the list

```csharp
public static IQueryable<T> Page<T>(
this IQueryable<T> query,
int pageNumZeroStart, int pageSize)
{
if (pageSize == 0)
throw new ArgumentOutOfRangeException
(nameof(pageSize), "pageSize cannot be zero.");
if (pageNumZeroStart != 0)
query = query
.Skip(pageNumZeroStart * pageSize);
return query.Take(pageSize);
}
```

![1690124168726](image/readme/1690124168726.png)

The ListBookService class providing a sorted, filtered, and paged list

```csharp
public class ListBooksService
{
private readonly EfCoreContext _context;
public ListBooksService(EfCoreContext context)
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
.FilterBooksBy(options.FilterBy,
options.FilterValue);
options.SetupRestOfDto(booksQuery);
return booksQuery.Page(options.PageNum-1,
options.PageSize);
}
}
```

![1690124273484](image/readme/1690124273484.png)

#### Introducing EF Core's entity State

1. Added—The entity needs to be created in the database. SaveChanges inserts it.
2. Unchanged—The entity exists in the database and hasn’t been modified on the client. SaveChanges ignores it.
3. Modified—The entity exists in the database and has been modified on the client. SaveChanges updates it.
4. Deleted—The entity exists in the database but should be deleted. SaveChanges deletes it.
5. Detached—The entity you provided isn’t tracked. SaveChanges doesn’t see it.

**Creating a single entity on its own**

1 Add the entity to the application’s DbContext.
2 Call the application’s DbContext’s SaveChanges method.

```csharp
var itemToAdd = new ExampleEntity
{
MyMessage = "Hello World"
};
context.Add(itemToAdd);
context.SaveChanges();
```

![1690124666784](image/readme/1690124666784.png)

**SQL commands created to insert a new row into the SingleEntities table**

```
SET NOCOUNT ON;
INSERT INTO ExampleEntities]
([MyMessage]) VALUES (@p0);
SELECT [ExampleEntityId]
FROM [ExampleEntities]
WHERE @@ROWCOUNT = 1 AND
[ExampleEntityId] = scope_identity();
```

Adding a Book entity class also adds any linked entity classes

```csharp
var book = new Book
{
Title = "Test Book",
PublishedOn = DateTime.Today,
Reviews = new List<Review>()
{
new Review
{
NumStars = 5,
Comment = "Great test book!",
VoterName = "Mr U Test"}}};
context.Add(book);
context.SaveChanges();
```

![1690126499868](image/readme/1690126499868.png)

 *Adding a Book with an existing Author*

```csharp
var foundAuthor = context.Authors
.SingleOrDefault(author => author.Name == "Mr. A");
if (foundAuthor == null)
throw new Exception("Author not found");
var book = new Book
{
Title = "Test Book",
PublishedOn = DateTime.Today
};
book.AuthorsLink = new List<BookAuthor>
{
new BookAuthor
{
Book = book,
Author = foundAuthor
}
};
context.Add(book);
context.SaveChanges();
```

![1690127174435](image/readme/1690127174435.png)

The first four lines load an Author entity with some checks to make sure that it was found; this Author class instance is tracked, so EF Core knows that it is already in the database. You create a new Book entity and add a new BookAuthor linking entity, but
instead of creating a new Author entity instance, you use the Author entity that you read in from the database. Because EF Core is tracking the Author instance and knows that it's in the database, EF Core won't try to add it again to the database when *SaveChanges*

### **Updating database rows**

Updating a database row is achieved in three stages:
1 Read the data (database row), possibly with some relationships.
2 Change one or more properties (database columns).
3 Write the changes back to the database (update the row).

**Updating Quantum Networking’s publication date**

```csharp
var book = context.Books
.SingleOrDefault(p =>
p.Title == "Quantum Networking");

if (book == null)
throw new Exception("Book not found");

book.PublishedOn = new DateTime(2058, 1, 1);

context.SaveChanges();
```

![1690127494610](image/readme/1690127494610.png)

*Handling disconnected updates in a web application*

1. The first stage is an initial read, done in one instance of the application's **DbContext**.
2. The second stage applies the update by using a new instance of the application's **DbContext**.

```csharp
var book = Context.Books.Find(BookId);
book.PublishedOn = PublishDate;
context.SaveChanges();
```

![1690127728438](image/readme/1690127728438.png)

ChangePubDateDto sends data to and receives it from the user

```csharp
public class ChangePubDateDto
{
public int BookId { get; set; }
public string Title { get; set; }
[DataType(DataType.Date)]
public DateTime PublishedOn { get; set; }
}
```

![1690127829615](image/readme/1690127829615.png)

**The Book entity class, showing the relationships to update**

```csharp
public class Book
{
   public int BookId { get; set; }
 //-----------------------------------------------
//relationships
   public PriceOffer Promotion { get; set; }
   public ICollection<Review> Reviews { get; set; }
   public ICollection<Tag> Tags { get; set; }
   public ICollection<BookAuthor> AuthorsLink { get; set; }
}
```

![1690128323618](image/readme/1690128323618.png)

![1690128352429](image/readme/1690128352429.png)

**Principal and dependent relationships**

The terms principal and dependent are used in EF to define parts of a relationship:

1. Principal entity—Contains a primary key that the dependent relationship refer to via a foreign key
2. Dependent entity—Contains the foreign key that refers to the principal entity's primary key

The Book entity class is the principal entity. The **PriceOffer, Review**, and **BookAuthor** entity classes are the dependent entities.

I find the terms principal and dependent to be helpful, because they define what's in charge: the principal entity. I use these terms throughout this book where applicable.

**NOTE** An entity class can be both a principal and a dependent entity at the same time. In a hierarchical relationship of, say, libraries with books that have reviews, the book would be a dependent relationship of the library entity class.

**Updating one-to-one relationships Adding a PriceOffer to a book**

```csharp
public class PriceOffer
{
public int PriceOfferId { get; set; }
public decimal NewPrice { get; set; }
public string PromotionalText { get; set; }
//-----------------------------------------------
//Relationships
public int BookId { get; set; }
}
```

![1690158267737](image/readme/1690158267737.png)

**Adding a new promotional price to an existing book that doesn't have one**

```csharp
var book = context.Books
.Include(p => p.Promotion)
.First(p => p.Promotion == null);
book.Promotion = new PriceOffer
{
NewPrice = book.Price / 2,
PromotionalText = "Half price today!"
};
context.SaveChanges();
```

![1690158489583](image/readme/1690158489583.png)

**ChangePriceOfferService class with a method to handle each stage**

```csharp
public class ChangePriceOfferService : IChangePriceOfferService
{
private readonly EfCoreContext _context;
public Book OrgBook { get; private set; }
public ChangePriceOfferService(EfCoreContext context)
{
_context = context;
}
public PriceOffer GetOriginal(int id)
{
OrgBook = _context.Books
.Include(r => r.Promotion)
.Single(k => k.BookId == id);
return OrgBook?.Promotion
?? new PriceOffer
{
BookId = id,
NewPrice = OrgBook.Price
};
}
public Book AddUpdatePriceOffer(PriceOffer promotion)
{
var book = _context.Books
.Include(r => r.Promotion)
.Single(k => k.BookId
== promotion.BookId);
if (book.Promotion == null)
{
book.Promotion = promotion;
}
else
{
book.Promotion.NewPrice
= promotion.NewPrice;
book.Promotion.PromotionalText
= promotion.PromotionalText;
}
_context.SaveChanges();
return book;
}
}
```

![1690158693344](image/readme/1690158693344.png)

Creating a PriceOffer row to go with an existing book

```csharp
var book = context.Books
.First(p => p.Promotion == null);
context.Add( new PriceOffer
{
BookId = book.BookId,
NewPrice = book.Price / 2,
PromotionalText = "Half price today!"
});
context.SaveChanges();
```

![1690158994138](image/readme/1690158994138.png)

**Updating one-to-many relationships: Adding a review to a book**

```csharp
public class Review
{
public int ReviewId { get; set; }
public string VoterName { get; set; }
public int NumStars { get; set; }
public string Comment { get; set; }
//-----------------------------------------
//Relationships
public int BookId { get; set; }
}
```

![1690159275908](image/readme/1690159275908.png)

**The Review class, showing the foreign key back to the Book entity class**

```csharp
public class Review
{
public int ReviewId { get; set; }
public string VoterName { get; set; }
public int NumStars { get; set; }
public string Comment { get; set; }
//-----------------------------------------
//Relationships
public int BookId { get; set; }
}
```

![1690159452207](image/readme/1690159452207.png)

**Adding a review to a book in the connected state**

```csharp
var book = context.Books
.Include(p => p.Reviews)
.First();
book.Reviews.Add(new Review
{
VoterName = "Unit Test",
NumStars = 5,
Comment = "Great book!"
});
context.SaveChanges();
```

![1690159958830](image/readme/1690159958830.png)

**Replacing a whole collection of reviews with another collection**

```csharp
var book = context.Books
.Include(p => p.Reviews)
.Single(p => p.BookId == twoReviewBookId);
book.Reviews = new List<Review>
{
new Review
{
VoterName = "Unit Test",
NumStars = 5,
}
};
context.SaveChanges();
```

![1690160071691](image/readme/1690160071691.png)

Adding a new review to a book in the example Book App

```csharp
public class AddReviewService
{
private readonly EfCoreContext _context;
public string BookTitle { get; private set; }
public AddReviewService(EfCoreContext context)
{
_context = context;
}
public Review GetBlankReview(int id)
{
BookTitle = _context.Books
.Where(p => p.BookId == id)
.Select(p => p.Title)
.Single();
return new Review
{
BookId = id
};
}
public Book AddReviewToBook(Review review)
{
var book = _context.Books
.Include(r => r.Reviews)
.Single(k => k.BookId
== review.BookId);
book.Reviews.Add(review);
_context.SaveChanges();
return book;
}
}
```

![1690160303906](image/readme/1690160303906.png)
