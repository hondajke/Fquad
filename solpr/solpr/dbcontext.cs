using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace solpr
{
    public partial class ParkDBEntities : DbContext
    {

        public ParkDBEntities():base("FquadExpress")
        {

        }
            

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }


        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Periphery> Peripheries { get; set; }
        public virtual DbSet<Computer> Computers { get; set; }
        public virtual DbSet<Specs> Specs { get; set; }


    }
}
