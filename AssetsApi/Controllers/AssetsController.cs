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
using System.IO;
using System.Net.Http;
using System.Net;
using AssetsApi.App_Code;
using System.Net.Http.Headers;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace AssetsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("Allow")]
    public class AssetsController : ControllerBase
    {
        private readonly AssetsContext _context;

        public AssetsController(AssetsContext context, IConfiguration iConfig)
        {
            //_context = context;

            var optionsBuilder = new DbContextOptionsBuilder<AssetsContext>();
            optionsBuilder.UseSqlServer(iConfig.GetConnectionString("DefaultConnection"));
            _context = new AssetsContext(optionsBuilder.Options);
        }

        // GET: api/Assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            return await _context.Assets
                .Include(x => x.State)
                .Include(x => x.Location)
                .Include(x => x.AcquisitionMethod)
                .Include(x => x.Depreciation)
                .Include(x => x.Responsible)
                .Include(x => x.Depreciation)
                .Where(x => x.Status == 1)
                .OrderBy(x => x.AssetId)
                .ToListAsync();
        }

        // GET: api/Assets
        [HttpGet("deleted")]
        public async Task<ActionResult<IEnumerable<Asset>>> GetDeletedAssets()
        {
            return await _context.Assets
                .Include(x => x.State)
                .Include(x => x.Location)
                .Include(x => x.AcquisitionMethod)
                .Include(x => x.Depreciation)
                .Include(x => x.Responsible)
                .Include(x => x.Depreciation)
                .Where(x => x.Status == 0)
                .OrderBy(x => x.AssetId)
                .ToListAsync();
        }


        [HttpGet("location/{location}")]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssetsByLocation(int location)
        {
            return await _context.Assets
                .Include(x => x.Location)
                .Include(x => x.State)
                .Include(x => x.Responsible)
                .Include(x => x.Responsible2)
                .Where(x => x.Location.ID == location && x.Status == 1)
                .OrderBy(x => x.AssetId)
                .ToListAsync();
        }

        [HttpGet("person/{person}")]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssetsByPerson(int person)
        {
            return await _context.Assets
                .Include(x => x.Location)
                .Include(x => x.State)
                .Include(x => x.Responsible)
                .Include(x => x.Responsible2)
                .OrderBy(x => x.AssetId)
                .Where(x => (x.Responsible.ID == person || x.Responsible2.ID == person) && x.Status == 1).OrderBy(x => x.Responsible.LastName).ToListAsync();
        }

        // GET: api/Assets
        [HttpGet("{index}/{pageSize}")]
        public IPagedList<Asset> GetAssets(int index, int pageSize)
        {
            try
            {
                return _context.Assets
                    .Where(x => x.Status == 1)
                    .OrderBy(x => x.AssetId)
                    .ToPagedList(index, pageSize);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpGet("export/{type}")]
        public async Task<FileContentResult> ExportMainReport(string type)
        {
            var path = "";
            try
            {
                var helper = new ExcelHelper();

                List<Asset> assets = null;
                switch (type)
                {
                    default:
                    case "general":
                        assets = _context.Assets
                        .Include(x => x.Location)
                        .Include(x => x.AcquisitionMethod)
                        .Include(x => x.State)
                        .OrderBy(x => x.AssetId)
                        .Where(x => x.Status == 1)
                        .ToList();
                        break;
                    case "deleted":
                        assets = _context.Assets
                        .Include(x => x.Location)
                        .Include(x => x.AcquisitionMethod)
                        .Include(x => x.State)
                        .OrderBy(x => x.AssetId)
                        .Where(x => x.Status == 0)
                        .ToList();
                        break;
                    case "all":
                        assets = _context.Assets
                        .Include(x => x.Location)
                        .Include(x => x.AcquisitionMethod)
                        .Include(x => x.State)
                        .OrderBy(x => x.AssetId)
                        .Where(x => x.Status == 1)
                        .ToList();

                        assets.AddRange(_context.Assets
                        .Include(x => x.Location)
                        .Include(x => x.AcquisitionMethod)
                        .Include(x => x.State)
                        .OrderBy(x => x.AssetId)
                        .Where(x => x.Status == 0)
                        .ToList());

                        break;

                }

                if (assets != null)
                {
                    path = helper.ExportData(assets);

                    var date = DateTime.Now.ToString().Replace('\\', '-');

                    Response.ContentType = "application/ms-excel";
                    Response.HttpContext.Response.Headers.Append("Content-Disposition", "attachment");

                    var file = System.IO.File.ReadAllBytes(path);

                    var fs = System.IO.File.OpenRead(path);

                    MemoryStream ms = new MemoryStream();
                    fs.CopyTo(ms);

                    var b = ms.ToArray();
                    fs.Close();
                    ms.Close();

                    return new FileContentResult(b, "application/ms-excel")
                    {
                        FileDownloadName = $"Reporte de Activos {date}.xlsx"
                    };
                }
                else
                {

                    throw new Exception("Error - No data to export");
                }
            }
            catch (Exception ex)
            {
                _ = Response.WriteAsync($"Error - {ex.Message}<br>");
                _ = Response.WriteAsync($"Stack - {ex.StackTrace}<br>");
                throw;
            }
        }

        [HttpGet("depreciation")]
        public void updateDepreciation()
        {
            try
            {
                var assets = _context.Assets.Include(x => x.Depreciation).Where(x => x.Status == 1);
                foreach (Asset a in assets)
                {

                    if (!string.IsNullOrEmpty(a.Depreciation.Frequency))
                    {
                        if (a.CurrentPrice > 0 || a.AcquisitionDate < a.AcquisitionDate.AddYears(int.Parse(a.Depreciation.Frequency)))
                        {
                            var diff = (DateTime.Now - a.AcquisitionDate).TotalDays;
                            var daily = a.Depreciation.Percentage / 30;
                            a.CurrentPrice = a.PurchasePrice - (a.PurchasePrice * ((daily * diff) / 100));
                            _context.Entry(a).State = EntityState.Modified;
                        }
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("pages/{pageSize}")]
        public double GetPagesCount(int pageSize)
        {
            double pages = 0;

            int rem = 0;

            pages = Math.DivRem(_context.Assets.Where(x => x.Status == 1).Count(), pageSize, out rem);

            if (rem > 0)
            {
                pages++;
            }

            return pages;
        }


        [HttpGet("assingBook/{tomo}/{folio}/{asiento}")]
        public bool AssignFinatialBook(int tomo, int folio, int asiento)
        {
            bool success = false;

            int currentAsiento = asiento - 1;
            int maxAsiento = 24;
            var assets = _context.Assets.Include(x => x.Location)
                .Where(x => x.Status == 1)
                .OrderBy(x => x.AssetId)
                .ToList();
            foreach (var a in assets)
            {
                if (currentAsiento == maxAsiento)
                {
                    folio++;
                    currentAsiento = 1;
                }
                else
                {
                    currentAsiento++;
                }
                a.Tomo = tomo;
                a.Folio = folio;
                a.Assiento = currentAsiento;
            }
            var res = _context.SaveChanges();
            if (res == 0)
                success = true;

            return success;
        }


        // GET: api/Assets
        [HttpGet("NextId")]
        public Asset GetNextId()
        {
            var asset = _context.Assets.OrderBy(x => x.AssetId).Last();
            var next = 0;
            string id = "";
            var ceros = 0;
            if (asset.AssetId.Contains("-"))
            {
                var its = asset.AssetId.Split('-');
                foreach (char c in its[1])
                {
                    if (c == '0')
                        ceros++;
                }
                next = int.Parse(its[1]) + 1;
                for (int i = 0; i < ceros; i++)
                {
                    id += "0";
                }
                id += next;
                asset.AssetId = $"{id}";
            }
            else
            {
                next = int.Parse(asset.AssetId) + 1;
                foreach (char c in asset.AssetId)
                {
                    if (c == '0')
                        ceros++;
                }
                for (int i = 0; i < ceros; i++)
                {
                    id += "0";
                }
                id += next;
                asset.AssetId = $"{id}";
            }

            return asset;
        }

        // GET: api/Assets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(long id)
        {
            var asset = _context.Assets
                .Include(x => x.State)
                .Include(x => x.Location)
                .Include(x => x.AcquisitionMethod)
                .Include(x => x.Depreciation)
                .Include(x => x.Responsible)
                .Include(x => x.Depreciation)
                .Include(x => x.Provider)
                .FirstOrDefault(x => x.Id == id);


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

            var props = typeof(Asset).GetProperties();

            _context.AssetsHistory.Add(new AssetHistory()
            { AssetID = asset.Id, NewDetails = asset.ToString(), PreviewsDetails = _asset.ToString(), UpdateDate = DateTime.Now, Action = "Edit" });

            foreach (var prop in props)
            {
                prop.SetValue(_asset, prop.GetValue(asset));
            }

            _context.Entry(_asset).State = EntityState.Modified;

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
            try
            {
                var acq = _context.AcquisitionMethods.FirstOrDefault(x => x.ID == asset.AcquisitionMethod.ID);
                var loc = _context.Locations.FirstOrDefault(x => x.ID == asset.Location.ID);
                var sta = _context.States.FirstOrDefault(x => x.ID == asset.State.ID);
                var dep = _context.Depreciations.FirstOrDefault(x => x.Id == asset.Depreciation.Id);
                var resp = _context.Persons.FirstOrDefault(x => x.ID == asset.Responsible.ID);
                var prov = _context.Providers.FirstOrDefault(x => x.ID == asset.Provider.ID);
                asset.Status = 1; //Active
                asset.AcquisitionMethod = acq;
                asset.Location = loc;
                asset.State = sta;
                asset.Depreciation = dep;
                asset.Responsible = resp;
                asset.Provider = prov;
                asset.LastUpdated = DateTime.Now;
                asset.PurchaseDate = DateTime.Now;
                asset.AcquisitionDate = asset.AcquisitionDate != null ? asset.AcquisitionDate.ToUniversalTime() : DateTime.Now;
                _context.Assets.Add(asset);

                _context.AssetsHistory.Add(new AssetHistory()
                { AssetID = asset.Id, NewDetails = asset.ToString(), PreviewsDetails = "Asset Creation", UpdateDate = DateTime.Now, Action = "Asset Creation" });
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetAsset", new { id = asset.Id }, asset);
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
            { AssetID = asset.Id, NewDetails = asset.ToString(), PreviewsDetails = "Asset Removal", UpdateDate = DateTime.Now, Action = "Asset Removal" });
            await _context.SaveChangesAsync();

            return asset;
        }

        private bool AssetExists(long id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }
    }
}
