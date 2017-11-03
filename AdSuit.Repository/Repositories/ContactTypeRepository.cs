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
    public class ContactTypeRepository: GenericRepository<ContactTypes>, IContactTypeRepository
    {
        public ContactTypeRepository(DbContext context)
           : base(context)
        {

        }
    }
}
