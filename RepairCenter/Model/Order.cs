using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int RepairTypeId { get; set; }
        public virtual RepairType RepairType { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int RequestId { get; set; }
        public virtual Request Request { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }

        public override string ToString()
        {
            return $"{this.Request.ClientName} - {this.Request.Device}";
        }
    }
}
