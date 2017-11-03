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
    public class EmployeeTagsService : EntityService<EmployeeTags>, IEmployeeTagsService
    {
        IUnitOfWork _unitOfWork;
        IEmployeeTagsRepository _employeeTagsRepository;

        public EmployeeTagsService(IUnitOfWork unitOfWork,
            IEmployeeTagsRepository employeeTagsRepository)
            : base(unitOfWork, employeeTagsRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeTagsRepository = employeeTagsRepository;
        }
    }
}
