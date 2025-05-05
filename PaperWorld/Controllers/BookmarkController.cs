using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requires authentication
public class BookmarksController : ControllerBase
{
    private readonly UserManager<Members> _userManager;  // Use IdentityUser
    private readonly DatabaseHandlerEfCoreExample _context;

    public BookmarksController(UserManager<Members> userManager, DatabaseHandlerEfCoreExample context)
    {
        _userManager = userManager;
        _context = context;
    }

    // Adds a book to the logged-in user's bookmarks.
    [HttpPost("{bookId}")]
    public async Task<IActionResult> AddBookmark(int bookId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized("User not found.");

        bool alreadyBookmarked = await _context.Bookmarks
            .AnyAsync(b => b.MemberId == user.Id && b.BookId == bookId);

        if (alreadyBookmarked)
            return BadRequest("This book is already bookmarked.");

        var bookmark = new Bookmarks
        {
            MemberId = user.Id,  // Use IdentityUser.Id
            BookId = bookId
        };

        _context.Bookmarks.Add(bookmark);
        await _context.SaveChangesAsync();

        return Ok("Bookmarked successfully.");
    }

    // Gets all bookmarks for the currently logged-in user.
    [HttpGet]
    public async Task<IActionResult> GetMyBookmarks()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized("User not found.");

        var bookmarks = await _context.Bookmarks
            .Where(b => b.MemberId == user.Id)  // Use IdentityUser.Id
            .Include(b => b.Book)
            .ToListAsync();

        return Ok(bookmarks);
    }
}
