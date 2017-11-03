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
    public class EmployeeHistoryService : EntityService<EmployeeHistories>, IEmployeeHistoryService
    {
        IUnitOfWork _unitOfWork;
        IEmployeeHistoryRepository _employeesRepository;

        public EmployeeHistoryService(IUnitOfWork unitOfWork,
            IEmployeeHistoryRepository employeesRepository)
            : base(unitOfWork, employeesRepository)
        {
            _unitOfWork = unitOfWork;
            _employeesRepository = employeesRepository;
        }
    }
}
