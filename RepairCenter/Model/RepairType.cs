using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Model
{
    public class RepairType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LeadTime { get; set; }
        public Decimal Price { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
