using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaperWorld.Models;

namespace PaperWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize] 
    public class MemberLibraryController : ControllerBase
    {
        private readonly DatabaseHandlerEfCoreExample _context;

        public MemberLibraryController(DatabaseHandlerEfCoreExample context)
        {
            _context = context;
        }

        // GET: api/MemberLibrary
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Books>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // GET: api/MemberLibrary
        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            return book;
        }
    }
}
