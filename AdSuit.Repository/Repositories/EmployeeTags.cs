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

    public class EmployeeTagsRepository : GenericRepository<EmployeeTags>, IEmployeeTagsRepository
    {
        public EmployeeTagsRepository(DbContext context)
              : base(context)
        {

        }
    }
}
