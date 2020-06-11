using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsApi.Models
{
    public class AssetNotes
    {
        public long IdNote { get; set; }
        public long AssetId { get; set; }
        public string Note { get; set; }
        public string Image { get; set; }
    }
}
