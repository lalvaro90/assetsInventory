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
    [ApiController]
    [Route("[controller]")]
    [EnableCors("Allow")]
    public class LocationsController : ControllerBase
    {
        private readonly AssetsContext _context;

        public LocationsController(AssetsContext context)
        {
            _context = context;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            return await _context.Locations
                .Include(x => x.Responsible1)
                .Include(x => x.Responsible2)
                .Where(x=> x.Status == 1).ToListAsync();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _context.Locations
                .Include(x => x.Responsible1)
                .Include(x => x.Responsible2)
                .FirstOrDefaultAsync(x=> x.ID == id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        private void setReponsibleByLocation(Location location, bool one = true, bool two = true) {
            var assets = _context.Assets
                .Include(x => x.Responsible).AsNoTracking()
                .Include(x => x.Responsible2).AsNoTracking()
                .Include(x=> x.Location)
                .Where(x => x.Location.ID == location.ID && x.Status == 1).ToList();
            foreach (var asset in assets)
            {
                if(one)
                asset.Responsible = location.Responsible1;
                if(two)
                asset.Responsible2 = location.Responsible2;
                _context.Entry(asset).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {
            if (id != location.ID)
            {
                return BadRequest();
            }

            var _old = _context.Locations.FirstOrDefault(x => x.ID == id);
            if (_old.Responsible1 == null && _old.Responsible2 == null)
            {
                setReponsibleByLocation(location);
            }
            else if (_old.Responsible1.ID != location.Responsible1.ID && _old.Responsible2.ID != location.Responsible1.ID
                && _old.Responsible1.ID != location.Responsible2.ID && _old.Responsible2.ID != location.Responsible2.ID)
            {
                setReponsibleByLocation(location);
            }
            else if (_old.Responsible1.ID != location.Responsible1.ID && _old.Responsible1.ID != location.Responsible2.ID) {
                setReponsibleByLocation(location, true, false);
            }
            else if (_old.Responsible2.ID != location.Responsible1.ID && _old.Responsible2.ID != location.Responsible2.ID)
            {
                setReponsibleByLocation(location, false, true);
            }
            _old.Responsible1 = location.Responsible1;
            _old.Responsible2 = location.Responsible2;

            _context.Entry(_old).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
            location.Status = 1; //Active;
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = location.ID }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Location>> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return location;
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.ID == id);
        }
    }
}
