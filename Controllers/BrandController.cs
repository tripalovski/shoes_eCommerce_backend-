using eCommerce_backend.Database;
using eCommerce_backend.DTOs;
using eCommerce_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_backend.Controllers
{
    [Route("api/brand/")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BrandController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: api/brand/
        [HttpGet("getAllBrands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrand() {
            return await _context.Brand
                .Select(b => new BrandDto {
                    Id = b.Id,
                    Name = b.Name,
                    Country = b.Country,
                    Description = b.Description,
                    Website = b.Website
                }).ToListAsync();
        }

        // GET: api/brand/5
        [HttpGet("getBrandByID/{id}")]
        public async Task<ActionResult<BrandDto>> GetBrand(int id) {
            var brand = await _context.Brand.FindAsync(id);

            if (brand == null) {
                return NotFound();
            }

            return new BrandDto {
                Id = brand.Id,
                Name = brand.Name,
                Country = brand.Country,
                Description = brand.Description,
                Website = brand.Website
            };
        }

        // POST: api/brand/createBrand
        [HttpPost("createBrand")]
        public async Task<ActionResult<BrandDto>> CreateBrand(CreateBrandDto dto) {
            var brand = new Brand {
                Name = dto.Name,
                Country = dto.Country,
                Description = dto.Description,
                Website = dto.Website
            };

            _context.Brand.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, new BrandDto {
                Id = brand.Id,
                Name = brand.Name,
                Country = brand.Country,
                Description = brand.Description,
                Website = brand.Website
            });
        }

        // PUT: api/brand/updateBrand/5
        [HttpPut("updateBrand/{id}")]
        public async Task<IActionResult> UpdateBrand(int id, CreateBrandDto dto) {
            var brand = await _context.Brand.FindAsync(id);
            if (brand == null) return NotFound();

            brand.Name = dto.Name;
            brand.Country = dto.Country;
            brand.Description = dto.Description;
            brand.Website = dto.Website;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/brand/deleteBrand/5
        [HttpDelete("deleteBrand/{id}")]
        public async Task<IActionResult> DeleteBrand(int id) {
            var brand = await _context.Brand.FindAsync(id);
            if (brand == null) return NotFound();

            _context.Brand.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
