using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetsApi.Models
{
    public class States
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
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
