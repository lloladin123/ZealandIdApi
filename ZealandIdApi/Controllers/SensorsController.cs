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
    public class SensorsController : ControllerBase
    {
        private readonly ZealandIdDbContext _context;

        public SensorsController(ZealandIdDbContext context)
        {
            _context = context;
        }

        [HttpPost("resetTable")]
        public IActionResult ResetSensorer()
        {
            try
            {
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Sensorer");
                _context.SaveChanges();

                //_context.Sensorer.AddRange(new List<Sensor> { new Sensor("ZA1", 1) });
                //_context.SaveChanges();

                return Ok("Sensorer table reset successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error resetting Sensorer table: {ex.Message}");
            }
        }

        // GET: api/Sensors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensor>>> GetSensorer()
        {
          if (_context.Sensorer == null)
          {
              return NotFound();
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
            var relatedEntity = await _context.lokaler.FindAsync(sensor.LokaleId);

            if(relatedEntity == null)
            {
                return StatusCode(422, "Invalid LokaleId");
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
