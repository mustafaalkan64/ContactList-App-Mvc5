using AdSuit.DAL;
//using AdSuit.DAL.DAL;
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
    public class TagRepository: GenericRepository<Tags>, ITagRepository
    {
        public TagRepository(DbContext context)
           : base(context)
        {

        }

        public override IEnumerable<Tags> GetAll()
        {
            return _entities.Set<Tags>().Include(x => x.EmployeeTags).AsEnumerable();
        }
    }
}
