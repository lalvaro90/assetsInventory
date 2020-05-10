using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsApi.Models
{
    public class Token
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expire { get; set; }
        public bool IsActive { get; set; }
        public int Type { get; set; }
    }
}
