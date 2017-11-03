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

    public class EmployeeHistoryRepository : GenericRepository<EmployeeHistories>, IEmployeeHistoryRepository
    {
        public EmployeeHistoryRepository(DbContext context)
              : base(context)
        {

        }
        public override IEnumerable<EmployeeHistories> GetAll()
        {
            return _entities.Set<EmployeeHistories>().AsEnumerable();
        }
    }
}
