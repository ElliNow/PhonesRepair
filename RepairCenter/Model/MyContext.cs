
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Model
{
    class MyContext : DbContext
    {
        //public System.Data.Entity.Infrastructure.DbChangeTracker ChangeTracker { get; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<RepairType> RepairTypes { get; set; }
        public DbSet<Request> Requests { get; set; }

        public MyContext()
        : base("DefaultConnection")
        { }
    }
}
