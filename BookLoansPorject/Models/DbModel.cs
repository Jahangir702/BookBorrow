using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookLoansPorject.Models
{
    public enum Genre { Fiction=1, NonFiction, History, Horror, Others}
    public class Book
    {
        public int BookId { get; set; }
        [Required, StringLength(150), Display(Name ="Book Title")]
        public string Title { get; set; }
        [Required, StringLength(150), Display(Name = "Author Name")]
        public string Author { get; set; }
        [Required, EnumDataType(typeof(Genre))]
        public Genre Genre { get; set; }
        [Required, StringLength(150)]
        public string BookCover { get; set; }
        public bool IsAvailable { get; set; }
        public virtual ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();

    }
    public class BookLoan
    {
        [Key]
        public int LoanId { get; set;}
        [Required, StringLength(150), Display(Name = "Borrower Name")]
        public string BorrowerName { get; set;}
        [Required, StringLength(150), Display(Name = "Borrower Address")]
        public string Address { get; set; }
        [Required, Display(Name = "Borrower Phone")]
        public int Phone { get; set; }
        [Required, Column(TypeName ="date")]
        public DateTime LoanDate { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime ReturnDate { get; set; }
        [Required, ForeignKey("Book")]
        public int BookId { get; set; } 
        public virtual Book Book { get; set; }

    }
    public class BookDbContext : DbContext
    {
        public BookDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Book>Books { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }
    }

    public class DbInitializer : DropCreateDatabaseIfModelChanges<BookDbContext>
    {

        protected override void Seed(BookDbContext context)
        {
            Book b = new Book { Title= "Ebong Himu", Author="Humayun Ahmed", Genre=Genre.Horror, BookCover="1.jpg", IsAvailable=true };
            b.BookLoans.Add(new BookLoan { BorrowerName = "Mostakim", Address = "Mirpur", Phone = 01728007783, LoanDate = DateTime.Parse("08-12-2023"), ReturnDate = DateTime.Parse("08-20-2023") });
            b.BookLoans.Add(new BookLoan { BorrowerName = "Naim", Address = "Kazipara", Phone = 01728007783, LoanDate = DateTime.Parse("08-11-2023"), ReturnDate = DateTime.Parse("08-15-2023") });
            context.Books.Add(b);
            context.SaveChanges();
        }
    }
}