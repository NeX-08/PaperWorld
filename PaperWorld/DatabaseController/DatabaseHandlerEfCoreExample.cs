using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaperWorld.Models;

public class DatabaseHandlerEfCoreExample : IdentityDbContext<Members>
{
    public DbSet<Books> Books { get; set; }
    public DbSet<Bookmarks> Bookmarks { get; set; }

    public DatabaseHandlerEfCoreExample(DbContextOptions<DatabaseHandlerEfCoreExample> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Bookmarks>()
            .HasOne(b => b.Member)
            .WithMany()
            .HasForeignKey(b => b.MemberId);

        builder.Entity<Bookmarks>()
            .HasOne(b => b.Book)
            .WithMany()
            .HasForeignKey(b => b.BookId);
    }

}