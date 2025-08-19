using eCommerce_backend.Database;
using eCommerce_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootwearController : ControllerBase
    {
        private readonly FootwearDbContext _context;

        public FootwearController(FootwearDbContext context) {
            _context = context;
        }

        // GET: api/Footwear
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Footwear>>> GetFootwear() {
            return await _context.Footwear.ToListAsync();
        }

        // GET: api/Footwear/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Footwear>> GetFootwear(int id) {
            var footwear = await _context.Footwear.FindAsync(id);

            if (footwear == null) {
                return NotFound();
            }

            return footwear;
        }

        // POST: api/Footwear
        [HttpPost]
        public async Task<ActionResult<Footwear>> PostFootwear(Footwear footwear) {
            _context.Footwear.Add(footwear);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFootwear), new { id = footwear.Id }, footwear);
        }

        // PUT: api/Footwear/5
        [HttpPut("{id}")]
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

        // DELETE: api/Footwear/5
        [HttpDelete("{id}")]
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
