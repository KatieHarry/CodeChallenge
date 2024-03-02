using System;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Repositories.Employees
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Compensation AddCompensation(Compensation compensation)
        {
            if (GetById(compensation.Employee.EmployeeId) != null)
            {
                _employeeContext.Compensations.Add(compensation);
                return compensation;
            }
            _logger.LogError("Employee not found");
            return null;
        }

        public Employee GetById(string id)
        {
            Employee employee = _employeeContext.Employees.Include(x => x.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
            return employee;
        }

        public Compensation GetCompensationById(string id)
        {
            return _employeeContext.Compensations.Include(x => x.Employee).OrderByDescending(x => x.EffectiveDate).SingleOrDefault(e => e.Employee.EmployeeId == id);
        }

        public ReportingStructure GetReports(string id)
        {
            var employee = GetById(id);
            var reports = GetAllEmployeeReports(employee);

            return new ReportingStructure
            {
                Employee = employee,
                DirectReports = reports
            };

        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }

        private int GetAllEmployeeReports(Employee employee)
        {
            if (employee.DirectReports == null || employee.DirectReports.Count == 0)
            {
                return 0; // Base case: Employee with no direct reports
            }

            int totalReports = employee.DirectReports.Count; // Count direct reports
            foreach (var subordinate in employee.DirectReports)
            {
                var report = GetById(subordinate.EmployeeId);
                totalReports += GetAllEmployeeReports(report); // Add reports of each subordinate
            }
            return totalReports;
        }
    }
}
