using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetsApi.Models
{
    public class Asset
    {
        public long Id { get; set; }
        public string AssetId { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Series { get; set; }
        public States State { get; set; }
        public Location Location { get; set; }
        public double PurchasePrice { get; set; }
        public double CurrentPrice { get; set; }
        public AcquisitionMethod AcquisitionMethod { get; set; }
        public Depreciation Depreciation { get; set; }
        public Person Responsible { get; set; }
        public List<AssetNotes> Notes { get; set; }
        public int Tomo { get; set; }
        public int Folio { get; set; }
        public int Assiento { get; set; }



        //Admin Properties
        public int Status { get; set; }
        public DateTime LastUpdated { get; set; }

        public override string ToString()
        {
            var props = typeof(Asset).GetProperties();
            StringBuilder sb = new StringBuilder();

            foreach (var prop in props)
            {
                sb.AppendLine(string.Format("{0}: {1}", prop.Name, prop.GetValue(this)));
            }

            return sb.ToString();
        }

    }
}
