using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsApi.Models
{
    public class Configuration
    {
        public string IdPrefix { get; set; }
        public string Currency { get; set; }
        public string InstitutionLogo { get; set; }
        public string ParentLogo { get; set; }
        public int Tomo { get; set; } // un unico tomo
        public int Folio { get; set; } //max 500 por tomo 
        public int Assiento { get; set; } // max 24 por folio
        public DateTime ValidUntil { get; set; }
    }
}
