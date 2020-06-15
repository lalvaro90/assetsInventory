using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetsApi.Models;
using Microsoft.AspNetCore.Cors;

namespace AssetsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("Allow")]
    public class AssetNotesController : ControllerBase
    {
        private readonly AssetsContext _context;

        public AssetNotesController(AssetsContext context)
        {
            _context = context;
        }

        // GET: api/AssetNotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetNotes>>> GetAssetNotes()
        {
            return await _context.AssetNotes.ToListAsync();
        }

        // GET: api/AssetNotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetNotes>> GetAssetNotes(long id)
        {
            var assetNotes = await _context.AssetNotes.FindAsync(id);

            if (assetNotes == null)
            {
                return NotFound();
            }

            return assetNotes;
        }

        // PUT: api/AssetNotes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssetNotes(long id, AssetNotes assetNotes)
        {
            if (id != assetNotes.IdNote)
            {
                return BadRequest();
            }

            _context.Entry(assetNotes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetNotesExists(id))
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

        // POST: api/AssetNotes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AssetNotes>> PostAssetNotes(AssetNotes assetNotes)
        {
            _context.AssetNotes.Add(assetNotes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssetNotes", new { id = assetNotes.IdNote }, assetNotes);
        }

        // DELETE: api/AssetNotes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AssetNotes>> DeleteAssetNotes(long id)
        {
            var assetNotes = await _context.AssetNotes.FindAsync(id);
            if (assetNotes == null)
            {
                return NotFound();
            }

            _context.AssetNotes.Remove(assetNotes);
            await _context.SaveChangesAsync();

            return assetNotes;
        }

        private bool AssetNotesExists(long id)
        {
            return _context.AssetNotes.Any(e => e.IdNote == id);
        }
    }
}
