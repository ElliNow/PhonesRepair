using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Model
{
    public class Detail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Provider { get; set; }
        public int WaitTime { get; set; }
        public Decimal Price { get; set; }
        public bool InStock { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
