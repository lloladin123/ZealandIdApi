using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZealandIdApi.EDbContext;
using ZealandIdApi.Models;

namespace ZealandIdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LokalerController : ControllerBase
    {
        private readonly ZealandIdDbContext _context;

        public LokalerController(ZealandIdDbContext context)
        {
            _context = context;
        }

        // GET: api/Lokaler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lokale>>> Getlokaler()
        {
          if (_context.lokaler == null)
          {
              return NotFound();
          }
            return await _context.lokaler.ToListAsync();
        }

        // GET: api/Lokaler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lokale>> GetLokale(int id)
        {
          if (_context.lokaler == null)
          {
              return NotFound();
          }
            var lokale = await _context.lokaler.FindAsync(id);

            if (lokale == null)
            {
                return NotFound();
            }

            return lokale;
        }

        // PUT: api/Lokaler/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLokale(int id, Lokale lokale)
        {
            if (id != lokale.Id)
            {
                return BadRequest();
            }

            _context.Entry(lokale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LokaleExists(id))
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

        // POST: api/Lokaler
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lokale>> PostLokale(Lokale lokale)
        {
          if (_context.lokaler == null)
          {
              return Problem("Entity set 'ZealandIdDbContext.lokaler'  is null.");
          }
            _context.lokaler.Add(lokale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLokale", new { id = lokale.Id }, lokale);
        }

        // DELETE: api/Lokaler/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLokale(int id)
        {
            if (_context.lokaler == null)
            {
                return NotFound();
            }
            var lokale = await _context.lokaler.FindAsync(id);
            if (lokale == null)
            {
                return NotFound();
            }

            _context.lokaler.Remove(lokale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LokaleExists(int id)
        {
            return (_context.lokaler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
