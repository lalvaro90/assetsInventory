using System;
using System.Text;

namespace AssetsApi.Models
{
    public class Depreciation
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public Double Percentage { get; set; }
        public string Frequency { get; set; }
        public DateTime LastRun { get; set; }
        public DateTime NextRun { get; set; }
        public override string ToString()
        {
            var props = typeof(Depreciation).GetProperties();
            StringBuilder sb = new StringBuilder();

            foreach (var prop in props)
            {
                sb.AppendLine(string.Format("{0}: {1}", prop.Name, prop.GetValue(this)));
            }

            return sb.ToString();
        }

    }
}
