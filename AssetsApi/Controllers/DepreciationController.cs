using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetsApi.Models;

namespace AssetsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepreciationController : ControllerBase
    {
        private readonly AssetsContext _context;

        public DepreciationController(AssetsContext context)
        {
            _context = context;
        }

        // GET: api/Depreciation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Depreciation>>> GetDepreciations()
        {
            return await _context.Depreciations.ToListAsync();
        }

        // GET: api/Depreciation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Depreciation>> GetDepreciation(int id)
        {
            var depreciation = await _context.Depreciations.FindAsync(id);

            if (depreciation == null)
            {
                return NotFound();
            }

            return depreciation;
        }

        // GET: api/Depreciation/5/true
        [HttpGet("{id}/{project}")]
        public async Task<ActionResult<IEnumerable<Asset>>> GetDepreciation(int id,bool project)
        {
            var depreciation = await _context.Depreciations.FindAsync(id);
            var _assets = _context.Assets.Where(x => x.Depreciation.Id == id);
            string oldDetails, newDetails;
            foreach (var a in _assets) {
                var _newPrice = a.Price - (a.Price * depreciation.Percentage);
                oldDetails = a.ToString();
                a.Price = _newPrice;
                newDetails = a.ToString();
                if (!project) {
                    _context.AssetsHistory.Add(new AssetHistory() { AssetID=a.Id, NewDetails = newDetails, PreviewsDetails = oldDetails, UpdateDate = DateTime.Now });
                }
            }
            if (!project) {
                await _context.SaveChangesAsync();
            }

            if (depreciation == null)
            {
                return NotFound();
            }

            return _assets.ToList();
        }

        // PUT: api/Depreciation/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepreciation(int id, Depreciation depreciation)
        {
            if (id != depreciation.Id)
            {
                return BadRequest();
            }

            _context.Entry(depreciation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepreciationExists(id))
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

        // POST: api/Depreciation
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Depreciation>> PostDepreciation(Depreciation depreciation)
        {
            _context.Depreciations.Add(depreciation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepreciation", new { id = depreciation.Id }, depreciation);
        }

        // DELETE: api/Depreciation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Depreciation>> DeleteDepreciation(int id)
        {
            var depreciation = await _context.Depreciations.FindAsync(id);
            if (depreciation == null)
            {
                return NotFound();
            }

            _context.Depreciations.Remove(depreciation);
            await _context.SaveChangesAsync();

            return depreciation;
        }

        private bool DepreciationExists(int id)
        {
            return _context.Depreciations.Any(e => e.Id == id);
        }
    }
}
