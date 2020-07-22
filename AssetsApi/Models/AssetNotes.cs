using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsApi.Models
{
    public class AssetNotes
    {
        public long IdNote { get; set; }
        public Asset Asset { get; set; }
        public string Note { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
