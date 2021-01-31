using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Experience { get; set; }

        public override string ToString() => this.Name;
    }
}
