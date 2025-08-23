using eCommerce_backend.Database;
using eCommerce_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_backend.Controllers
{
    [Route("api/footwear")]
    [ApiController]
    public class FootwearController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FootwearController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: api/footwears
        [HttpGet("getAllFootwears")]
        public async Task<ActionResult<IEnumerable<Footwear>>> GetAllFootwears() {
            return await _context.Footwear.ToListAsync();
        }

        // GET: api/footwear/getFootwearByID/5
        [HttpGet("getFootwearByID/{id}")]
        public async Task<ActionResult<Footwear>> GetFootwear(int id) {
            var footwear = await _context.Footwear.FindAsync(id);

            if (footwear == null) {
                return NotFound();
            }

            return footwear;
        }

        // POST: api/footwear/createFootwear
        [HttpPost("createFootwear")]
        public async Task<ActionResult<Footwear>> PostFootwear(Footwear footwear) {
            _context.Footwear.Add(footwear);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFootwear), new { id = footwear.Id }, footwear);
        }

        // PUT: api/footwear/updateFootwear/5
        [HttpPut("updateFootwear/{id}")]
        public async Task<IActionResult> PutFootwear(int id, Footwear footwear) {
            if (id != footwear.Id) {
                return BadRequest();
            }

            _context.Entry(footwear).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!FootwearExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/footwear/deleteFootwear/5
        [HttpDelete("deleteFootwear/{id}")]
        public async Task<IActionResult> DeleteFootwear(int id) {
            var footwear = await _context.Footwear.FindAsync(id);
            if (footwear == null) {
                return NotFound();
            }

            _context.Footwear.Remove(footwear);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FootwearExists(int id) {
            return _context.Footwear.Any(e => e.Id == id);
        }
    }
}
