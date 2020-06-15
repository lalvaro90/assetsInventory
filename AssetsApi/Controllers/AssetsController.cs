using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetsApi.Models;
using Microsoft.AspNetCore.Cors;
using PagedList;

namespace AssetsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("Allow")]
    public class AssetsController : ControllerBase
    {
        private readonly AssetsContext _context;

        public AssetsController(AssetsContext context)
        {
            _context = new AssetsContext(new DbContextOptions<AssetsContext>()) ;
        }

        // GET: api/Assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            return await _context.Assets.Where(x=> x.Status == 1).ToListAsync();
        }


        // GET: api/Assets
        [HttpGet("{index}/{pageSize}")]
        public  IPagedList<Asset> GetAssets(int index, int pageSize)
        {
            return _context.Assets.ToPagedList(index,pageSize);
        }

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(long id)
        {
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            return asset;
        }

        // PUT: api/Assets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset(long id, Asset asset)
        {
            if (id != asset.Id)
            {
                return BadRequest();
            }

            var _asset = await _context.Assets.FindAsync(id);

            _context.AssetsHistory.Add(new AssetHistory()
            { AssetID = asset.Id, NewDetails = asset.ToString(), PreviewsDetails = _asset.ToString(), UpdateDate = DateTime.Now, Action = "Edit" });

            _context.Entry(asset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
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

        // PUT: api/Assets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("transfer/{id}")]
        public async Task<IActionResult> Transfer(long id, Asset asset)
        {
            if (id != asset.Id)
            {
                return BadRequest();
            }

            var _asset = await _context.Assets.FindAsync(id);

            _context.AssetsHistory.Add(new AssetHistory()
            { AssetID = asset.Id, NewDetails = asset.ToString(), PreviewsDetails = _asset.ToString(), UpdateDate = DateTime.Now, Action = "Asset Transfer" });

            _context.Entry(asset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
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

        // POST: api/Assets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Asset>> PostAsset(Asset asset)
        {
            asset.Status = 1; //Active
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();

            _context.AssetsHistory.Add(new AssetHistory()
            { AssetID = asset.Id, NewDetails = asset.ToString(), PreviewsDetails = "Asset Creation", UpdateDate = DateTime.Now , Action = "Asset Creation"});
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAsset", new { id = asset.Id }, asset);
        }

        // DELETE: api/Assets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Asset>> DeleteAsset(long id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            asset.Status = 0;
            await _context.SaveChangesAsync();

            _context.AssetsHistory.Add(new AssetHistory()
            { AssetID = asset.Id, NewDetails = asset.ToString(), PreviewsDetails = "Asset Removal", UpdateDate = DateTime.Now, Action = "Asset Removal"});
            await _context.SaveChangesAsync();

            return asset;
        }

        private bool AssetExists(long id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }
    }
}
