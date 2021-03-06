﻿using System;
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
            var props = typeof(States).GetProperties();
            StringBuilder sb = new StringBuilder();

            foreach (var prop in props)
            {
                var val = Convert.ChangeType(prop.GetValue(this), prop.PropertyType);

                sb.AppendLine(string.Format("{0}: {1}", prop.Name, prop.GetValue(this,null)));
            }

            return sb.ToString();
        }
    }
}
