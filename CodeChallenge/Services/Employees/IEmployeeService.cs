using CodeChallenge.Models;

namespace CodeChallenge.Services.Employees
{
    public interface IEmployeeService
    {
        Employee Create(Employee employee);
        Employee GetById(string id);
        ReportingStructure GetReports(string id);
        Employee Replace(Employee originalEmployee, Employee newEmployee);
    }
}
