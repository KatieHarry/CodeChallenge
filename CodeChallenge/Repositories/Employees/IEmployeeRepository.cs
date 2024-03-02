using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        Employee GetById(string id);
        ReportingStructure GetReports(string id);
        Employee Add(Employee employee);
        Employee Remove(Employee employee);
        Task SaveAsync();
    }
}