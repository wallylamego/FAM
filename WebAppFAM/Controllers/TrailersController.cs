using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Models;

namespace WebAppFAM.Controllers
{
    [Produces("application/json")]
    [Route("api/Trailers")]
    public class TrailersController : Controller
    {
        private readonly WebAppFAMContext _context;

        public TrailersController(WebAppFAMContext context)
        {
            _context = context;
        }

        // GET: api/Trailers
        [HttpGet]
        public IEnumerable<Trailer> GetTrailer()
        {
            return _context.Trailers;
        }

        // GET: api/Trailers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrailer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trailer = await _context.Trailers.SingleOrDefaultAsync(m => m.VehicleID == id);

            if (trailer == null)
            {
                return NotFound();
            }

            return Ok(trailer);
        }

        // PUT: api/Trailers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrailer([FromRoute] int id, [FromBody] Trailer trailer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trailer.VehicleID)
            {
                return BadRequest();
            }

            _context.Entry(trailer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrailerExists(id))
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

        // POST: api/Trailers
        [HttpPost]
        public async Task<IActionResult> PostTrailer([FromBody] Trailer trailer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trailers.Add(trailer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrailer", new { id = trailer.VehicleID }, trailer);
        }

        // DELETE: api/Trailers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrailer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trailer = await _context.Trailers.SingleOrDefaultAsync(m => m.VehicleID == id);
            if (trailer == null)
            {
                return NotFound();
            }

            _context.Trailers.Remove(trailer);
            await _context.SaveChangesAsync();

            return Ok(trailer);
        }

        private bool TrailerExists(int id)
        {
            return _context.Trailers.Any(e => e.VehicleID == id);
        }
    }
}