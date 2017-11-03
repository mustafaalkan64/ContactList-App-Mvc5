using AdSuit.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSuit.Repository.Interfaces
{
    public interface ITagRepository: IGenericRepository<Tags> 
    {
        //Tags GetById(int id);
    }
}
