using AdSuit.DAL.Models;
using AdSuit.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSuit.Repository.Repositories
{

    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context)
              : base(context)
        {

        }
        public override IEnumerable<Employee> GetAll()
        {
            return _entities.Set<Employee>().Include(x => x.EmployeeProperties).Include(x => x.EmployeeTags).Where(a => a.Deleted == false).AsEnumerable();
        }

        public override IQueryable<Employee> GetQueryableAll()
        {
            return _entities.Set<Employee>().Include(x => x.EmployeeProperties).Include(x => x.EmployeeTags).Where(a => a.Deleted == false).AsQueryable();
        }
    }
}
