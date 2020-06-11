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
    public class AcquisitionMethodsController : ControllerBase
    {
        private readonly AssetsContext _context;

        public AcquisitionMethodsController(AssetsContext context)
        {
            _context = context;
        }

        // GET: api/AcquisitionMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcquisitionMethod>>> GetAcquisitionMethods()
        {
            return await _context.AcquisitionMethods.ToListAsync();
        }

        // GET: api/AcquisitionMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AcquisitionMethod>> GetAcquisitionMethod(int id)
        {
            var acquisitionMethod = await _context.AcquisitionMethods.FindAsync(id);

            if (acquisitionMethod == null)
            {
                return NotFound();
            }

            return acquisitionMethod;
        }

        // PUT: api/AcquisitionMethods/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcquisitionMethod(int id, AcquisitionMethod acquisitionMethod)
        {
            if (id != acquisitionMethod.ID)
            {
                return BadRequest();
            }

            _context.Entry(acquisitionMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcquisitionMethodExists(id))
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

        // POST: api/AcquisitionMethods
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AcquisitionMethod>> PostAcquisitionMethod(AcquisitionMethod acquisitionMethod)
        {
            _context.AcquisitionMethods.Add(acquisitionMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAcquisitionMethod", new { id = acquisitionMethod.ID }, acquisitionMethod);
        }

        // DELETE: api/AcquisitionMethods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AcquisitionMethod>> DeleteAcquisitionMethod(int id)
        {
            var acquisitionMethod = await _context.AcquisitionMethods.FindAsync(id);
            if (acquisitionMethod == null)
            {
                return NotFound();
            }

            _context.AcquisitionMethods.Remove(acquisitionMethod);
            await _context.SaveChangesAsync();

            return acquisitionMethod;
        }

        private bool AcquisitionMethodExists(int id)
        {
            return _context.AcquisitionMethods.Any(e => e.ID == id);
        }
    }
}
