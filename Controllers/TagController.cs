using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Dto;
using ProductManagementAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly AppDbContext _context;

    public TagController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Tag
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTags()
    {
        return await _context.Tags
            .Select(tag => new TagDto
            {
                Id = tag.Id,
                Name = tag.Name
            })
            .ToListAsync();
    }
    // GET: api/Tag/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TagWithProductsDto>> GetTag(int id)
    {
        var tag = await _context.Tags
            .Include(t => t.Products)
            .Where(t => t.Id == id)
            .Select(t => new TagWithProductsDto
            {
                Id = t.Id,
                Name = t.Name,
                ProductNames = t.Products.Select(p => p.Name).ToList()
            })
            .FirstOrDefaultAsync();

        if (tag == null)
        {
            return NotFound();
        }

        return Ok(tag);
    }
    // POST: api/Tag
    [HttpPost]
    public async Task<ActionResult<TagDto>> CreateTag(CreateTagDto request)
    {
        var tag = new Tag
        {
            Name = request.Name
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, new TagDto
        {
            Id = tag.Id,
            Name = tag.Name
        });
    }
    // PUT: api/Tag/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag(int id, UpdateTagDto request)
    {
        if (id != request.Id)
        {
            return BadRequest("Tag ID mismatch.");
        }

        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
        {
            return NotFound();
        }

        tag.Name = request.Name;

        _context.Entry(tag).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Tags.Any(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }
    // DELETE: api/Tag/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
        {
            return NotFound();
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
