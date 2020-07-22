using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsApi.Models
{
    public class Configuration
    {
        public int IdConfig { get; set; }
        public string IdPrefix { get; set; }
        public string Currency { get; set; }
        public string InstitutionLogo { get; set; }
        public string ParentLogo { get; set; }
        public string InstituteName { get; set; }
        public string PhoneNumber { get; set; }
        public string Director { get; set; }
        public string Supervisor { get; set; }
        public string Circuito { get; set; }
        public int Tomo { get; set; } // un unico tomo
        public int Folio { get; set; } //max 500 por tomo 
        public int Assiento { get; set; } // max 24 por folio
        public DateTime ValidUntil { get; set; }

    }
}
