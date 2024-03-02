using System;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories.Employees;
using CodeChallenge.Repositories.Compensations;

namespace CodeChallenge.Services.Compensations
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository, IEmployeeRepository employeeRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        public Compensation Create(CompensationData compensationData)
        {
            var compensation = new Compensation();
            if (compensationData != null)
            {
                compensation.Salary = compensationData.Salary;
                compensation.EffectiveDate = compensationData.EffectiveDate;
                compensation.Employee = _employeeRepository.GetById(compensationData.EmployeeId);
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id);
            }

            return null;
        }
    }
}

public class CompensationData { 
    public string EmployeeId { get; set; }
    public int Salary { get; set;}
    public DateTime EffectiveDate { get; set; }
}
