using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using ZealandIdApi.EDbContext;
using ZealandIdApi.Models;

namespace ZealandIdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LokalerController : ControllerBase
    {
        private readonly ZealandIdDbContext _context;
        private readonly IConfiguration _configuration;

        public LokalerController(ZealandIdDbContext context, IConfiguration confirguration)
        {
            _context = context;
            _configuration = confirguration;
        }

        // BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK Udkommenter denne methode før release BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK
        [HttpPost("resetTable")]
        public IActionResult ResetLokaler()
        {
            string defaultConnection = _configuration.GetConnectionString("DefaultConnection");
            // Get the root path of the Web API application
            string rootPath = Directory.GetCurrentDirectory();

            string scriptFilePath = $"{rootPath}/SqlScripts/ResetSensorer.sql"; // Replace with the path to your SQL script file

            // Read the SQL script content from the file
            string scriptContent = System.IO.File.ReadAllText(scriptFilePath);

            // Run the SQL script
            ExecuteScript(defaultConnection, scriptContent);

            return (Ok("Database reset"));
        }

        static void ExecuteScript(string connectionString, string scriptContent)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(scriptContent, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // GET: api/Lokaler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lokale>>> Getlokaler()
        {
            if (_context.lokaler == null)
            {
                return NotFound("Der er nogen lokaler i databasen");
            }
            return await _context.lokaler.ToListAsync();
        }

        // GET: api/Lokaler/5
        [HttpGet("Id/{id}")]
        public async Task<ActionResult<Lokale>> GetLokale(int id)
        {
            if (_context.lokaler == null)
            {
                return NotFound("DbContext can'be null");
            }
            var lokale = await _context.lokaler.FindAsync(id);

            if (lokale == null)
            {
                return NotFound("Der er ikke noget lokale med det Id:" + id + "");
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
                    return NotFound("Der er ikke nogen lokaler med det Id:" + id + "");
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
            try
            {
                lokale.Validate();
            }
            catch (Exception ex)
            {
                return StatusCode(422, ex.Message);
            }
            if (_context.lokaler == null)
            {
                return Problem("Entity set 'ZealandIdDbContext.lokaler'  is null.");
            }
            if (_context.Sensorer.FindAsync(lokale.SensorId) == null)
            {
                return StatusCode(422, "Der er ikke nogen sensorer med det Id: "+ lokale.SensorId + "");
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
                return NotFound("Der er ikke noget lokale med det Id: " + id + "");
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
