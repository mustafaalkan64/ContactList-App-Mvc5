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
    public class EmployeeContactTypesService : EntityService<EmployeeProperties>, IEmployeeContactTypesService
    {
        IUnitOfWork _unitOfWork;
        IEmployeeContactTypeRepository _employeesRepository;

        public EmployeeContactTypesService(IUnitOfWork unitOfWork,
            IEmployeeContactTypeRepository employeesRepository)
            : base(unitOfWork, employeesRepository)
        {
            _unitOfWork = unitOfWork;
            _employeesRepository = employeesRepository;
        }
    }
}
