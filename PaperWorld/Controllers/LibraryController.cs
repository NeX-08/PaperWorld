using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaperWorld.Models;


    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DatabaseHandlerEfCoreExample _context;

    public LibraryController(UserManager<IdentityUser> userManager,
                             DatabaseHandlerEfCoreExample context)
    {
        _userManager = userManager;
        _context = context;
    }

    // GET: api/Books
    [Authorize(Roles = "AdminRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Books>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // GET: api/Books/id
        [Authorize(Roles = "AdminRole")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            return book;
        }

        // POST: api/Books
        [Authorize(Roles = "AdminRole")]
        [HttpPost]
        public async Task<ActionResult<Books>> CreateBook(Books book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // PUT: api/Books/id
        [Authorize(Roles = "AdminRole")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Books book)
        {
            if (id != book.Id)
                return BadRequest();

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Books/5
        [Authorize(Roles = "AdminRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
