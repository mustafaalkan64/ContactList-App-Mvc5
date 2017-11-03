using AdSuit.DAL.Models;
using AdSuit.Repository;
using AdSuit.Repository.Interfaces;
using AdSuit.Repository.Repositories;
using AdSuit.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSuit.Service.Services
{
    public class TagService : EntityService<Tags>, ITagService
    {
        IUnitOfWork _unitOfWork;
        ITagRepository _TagsRepository;
        //IEmployeeRepository _employeesRepository;

        public TagService(IUnitOfWork unitOfWork, ITagRepository TagsRepository)
            : base(unitOfWork, TagsRepository)
        {
            _unitOfWork = unitOfWork;
            _TagsRepository = TagsRepository;
        }

    }
}
