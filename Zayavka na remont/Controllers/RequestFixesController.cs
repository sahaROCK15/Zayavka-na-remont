using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zayavka_na_remont.Models;

namespace Zayavka_na_remont.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestFixesController : ControllerBase
    {
        private readonly DbContext _context;

        public RequestFixesController(DbContext context)
        {
            _context = context;
        }

        // GET: api/RequestFixes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestFix>>> GetRequestFix()
        {
          if (_context.RequestFix == null)
          {
              return NotFound();
          }
            return await _context.RequestFix.ToListAsync();
        }

        // GET: api/RequestFixes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestFix>> GetRequestFix(int id)
        {
          if (_context.RequestFix == null)
          {
              return NotFound();
          }
            var requestFix = await _context.RequestFix.FindAsync(id);

            if (requestFix == null)
            {
                return NotFound();
            }

            return requestFix;
        }

        // PUT: api/RequestFixes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestFix(int id, RequestFix requestFix)
        {
            if (id != requestFix.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestFix).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestFixExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RequestFixes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RequestFix>> PostRequestFix(RequestFix requestFix)
        {
          if (_context.RequestFix == null)
          {
              return Problem("Entity set 'DbContext.RequestFix'  is null.");
          }
            _context.RequestFix.Add(requestFix);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestFix", new { id = requestFix.Id }, requestFix);
        }

        // DELETE: api/RequestFixes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestFix(int id)
        {
            if (_context.RequestFix == null)
            {
                return NotFound();
            }
            var requestFix = await _context.RequestFix.FindAsync(id);
            if (requestFix == null)
            {
                return NotFound();
            }

            _context.RequestFix.Remove(requestFix);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestFixExists(int id)
        {
            return (_context.RequestFix?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
