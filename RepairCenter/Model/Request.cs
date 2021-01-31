using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Model
{
    public class Request
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string Device { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Device;
        }
    }
}
