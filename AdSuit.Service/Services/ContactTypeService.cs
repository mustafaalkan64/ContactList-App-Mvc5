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
    public class ContactTypeService : EntityService<ContactTypes>, IContactTypeService
    {
        IUnitOfWork _unitOfWork;
        IContactTypeRepository _ContactTypesRepository;

        public ContactTypeService(IUnitOfWork unitOfWork, IContactTypeRepository ContactTypesRepository)
            : base(unitOfWork, ContactTypesRepository)
        {
            _unitOfWork = unitOfWork;
            _ContactTypesRepository = ContactTypesRepository;
        }

    }
}
