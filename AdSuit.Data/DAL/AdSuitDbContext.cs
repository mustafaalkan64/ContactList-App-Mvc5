using AdSuit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSuit.Data.DAL
{
    public class AdSuitDbContext: DbContext
    {
        public AdSuitDbContext(): base("DefaultConnection")
        {

        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeHistories> EmployeeHistories { get; set; }
        public DbSet<EmployeeProperties> EmployeeProperties { get; set; }
        public DbSet<EmployeeTags> EmployeeTags { get; set; }
        public DbSet<ContactTypes> ContactTypes { get; set; }
        public DbSet<Tags> Tags { get; set; }
    }
}
