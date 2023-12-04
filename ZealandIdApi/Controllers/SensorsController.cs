using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ZealandIdApi.EDbContext;
using ZealandIdApi.Models;

namespace ZealandIdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ZealandIdDbContext _context;

        public SensorsController(ZealandIdDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK Udkommenter denne methode før release BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK BEMÆRK
        [HttpPost("resetTable")]
        public IActionResult ResetSensorer()
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

        // GET: api/Sensors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensorer()
        {
          if (_context.Sensorer == null)
          {
              return NotFound("der er ingen sensorer");
          }
            return await _context.Sensorer.ToListAsync();
        }

        // GET: api/Sensors/5
        [HttpGet("Id/{id}")]
        public async Task<ActionResult<Sensor>> GetSensor(int id)
        {
          if (_context.Sensorer == null)
          {
              return NotFound("DbContext can'be null");
          }
            var sensor = await _context.Sensorer.FindAsync(id);

            if (sensor == null)
            {
                return NotFound("No Such sensor exists");
            }
            return Ok(sensor);
        }

        // PUT: api/Sensors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensor(int id, Sensor sensor)
        {
            if (id != sensor.Id)
            {
                return BadRequest();
            }

            _context.Entry(sensor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorExists(id))
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

        // POST: api/Sensors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sensor>> PostSensor(Sensor sensor)
        {
          try
            {
                sensor.Validate();
            }
            catch (Exception ex)
            {
                return StatusCode(422, ex.Message);
            }
          if (_context.Sensorer == null)
          {
              return Problem("Entity set 'ZealandIdDbContext.Sensorer'  is null.");
          }
            _context.Sensorer.Add(sensor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSensor", new { id = sensor.Id }, sensor);
        }

        // DELETE: api/Sensors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            if (_context.Sensorer == null)
            {
                return NotFound();
            }
            var sensor = await _context.Sensorer.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }

            _context.Sensorer.Remove(sensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorExists(int id)
        {
            return (_context.Sensorer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
