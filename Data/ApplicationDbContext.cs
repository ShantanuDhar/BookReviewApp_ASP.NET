using BookReviewApp.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");

        
        modelBuilder.Entity<Book>().ToTable("Books");

        modelBuilder.Entity<Review>().ToTable("Reviews");
    }
    public async Task AddBook(string title, string author, string genre)
    {
        await Database.ExecuteSqlInterpolatedAsync($"EXEC AddBook {title}, {author}, {genre}");
    }

    public async Task<List<Book>> GetBooks()
    {
        return await Books.FromSqlRaw("EXEC GetBooks").ToListAsync();
    }
  
    //public async Task AddReview(int bookId, int userId, string reviewText, int rating)
    //{
    //    await Database.ExecuteSqlInterpolatedAsync($"EXEC AddReview {bookId}, {userId}, {reviewText}, {rating}");
    //}

    //public async Task<List<Review>> GetReviewsByBookId(int bookId)
    //{
    //    return await Reviews.FromSqlInterpolated($"EXEC GetReviewsByBookId {bookId}").ToListAsync();
    //}
}
