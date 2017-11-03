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

    public class EmployeeContactTypesRepository : GenericRepository<EmployeeProperties>, IEmployeeContactTypeRepository
    {
        public EmployeeContactTypesRepository(DbContext context)
              : base(context)
        {

        }
    }
}
