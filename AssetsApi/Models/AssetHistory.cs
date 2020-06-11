using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsApi.Models
{
    public class AssetHistory
    {
        public long Id { get; set; }
        public string Action { get; set; }

        public long AssetID { get; set; }
        public string PreviewsDetails { get; set; }
        public string NewDetails { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
