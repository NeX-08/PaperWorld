using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaperWorld.Models;

public class DatabaseHandlerEfCoreExample : IdentityDbContext<IdentityUser>
{
    public DatabaseHandlerEfCoreExample(DbContextOptions<DatabaseHandlerEfCoreExample> options)
        : base(options)
    {

    }
    public DbSet<Books> Books { get; set; }

}