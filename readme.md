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

For this MyFirstEfCoreApp application example, I created a simple database, shown in
figure  with only two tables:

1. A Books table holding the book informatio
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
