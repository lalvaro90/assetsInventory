using System;
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

    }
}
