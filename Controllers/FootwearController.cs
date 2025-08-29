using eCommerce_backend.Database;
using eCommerce_backend.DTOs;
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

        // GET: api/footwear/getAllFootwears
        [HttpGet("getAllFootwears")]
        public async Task<ActionResult<IEnumerable<FootwearDto>>> GetAllFootwears() {
            return await _context.Footwear
            .Include(f => f.Brand)
            .Select(f => new FootwearDto {
                Id = f.Id,
                Name = f.Name,
                Price = f.Price,
                Color = f.Color,
                Size = f.Size,
                Description = f.Description,
                ImageUrl = f.ImageUrl,
                Brand = f.Brand != null ? f.Brand.Name : string.Empty
            })
            .ToListAsync();
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
            var footwear = await _context.Footwear
                .Include(f => f.OrderItems)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (footwear == null) {
                return NotFound();
            }

            // First remove associated OrderItems
            _context.OrderItem.RemoveRange(footwear.OrderItems);
            _context.Footwear.Remove(footwear);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("getBrandList")]
        public async Task<ActionResult<IEnumerable<BrandSelectDto>>> GetBrandList() {
            //return await _context.Footwear.ToListAsync();
            return await _context.Brand
            .Select(b => new BrandSelectDto {
                Id = b.Id,
                Name = b.Name,
            })
            .ToListAsync();
        }


        private bool FootwearExists(int id) {
            return _context.Footwear.Any(e => e.Id == id);
        }

    }
}
