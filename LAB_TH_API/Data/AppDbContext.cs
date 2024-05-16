using LAB_TH_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LAB_TH_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Publishers> Publishers { get; set; }
        public DbSet<Book_Author> BooksAuthor { get; set;}
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book_Author>().HasKey(ba => new {ba.BookId, ba.AuthorId});
            modelBuilder.Entity<Book_Author>().HasOne(ba =>  ba.books)
                .WithMany(b => b.Book_Author)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<Book_Author>()
                .HasOne(ba => ba.authors)
                .WithMany(a => a.Book_Author)
                .HasForeignKey(ba => ba.AuthorId);

            modelBuilder.Entity<Books>()
                .HasOne(b => b.publishers)
                .WithMany(p => p.Book)
                .HasForeignKey(b => b.PublishersId);


            //seeddata
            modelBuilder.Entity<Publishers>().HasData(
            new Publishers { Id = 1, Name = "Publisher A" },
            new Publishers { Id = 2, Name = "Publisher B" }
             );

            modelBuilder.Entity<Authors>().HasData(
                new Authors { Id = 1, FullName = "Nguyen Van A" },
                new Authors { Id = 2, FullName = "Tran Van B" },
                 new Authors
                 {
                     Id = 3,
                     FullName = "Nguyễn Văn C"
                 }
            );

            modelBuilder.Entity<Books>().HasData(
                new Books
                {
                    Id = 1,
                    Title = "Book 1",
                    Description = "Description for Book 1",
                    IsRead = true,
                    DateRead = DateTime.Now,
                    Rate = 4,
                    Genre = 1,
                    CoverUrl = "url1",
                    DateAdded = DateTime.Now,
                    PublishersId = 1
                },
                new Books
                {
                    Id = 2,
                    Title = "Book 2",
                    Description = "Description for Book 2",
                    IsRead = false,
                    DateRead = DateTime.Now,
                    Rate = 3,
                    Genre = 2,
                    CoverUrl = "url2",
                    DateAdded = DateTime.Now,
                    PublishersId = 2
                },
                new Books
                {
                    Id = 3,
                    Title = "Book 3",
                    Description = "Description for Book 3",
                    IsRead = true,
                    DateRead = DateTime.Now,
                    Rate = 4,
                    Genre = 2,
                    CoverUrl = "url3",
                    DateAdded = DateTime.Now,
                    PublishersId = 2
                }
            );

            modelBuilder.Entity<Book_Author>().HasData(
                new Book_Author {Id = 1, BookId = 1, AuthorId = 1 },
                new Book_Author {Id = 2, BookId = 2, AuthorId = 2 },
                 new Book_Author {Id = 3, BookId = 2, AuthorId = 3 }
            );
        }
    }
}
