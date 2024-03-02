using CodeChallenge.Models;

namespace CodeChallenge.Services.Compensations
{
    public interface ICompensationService
    {
        Compensation Create(CompensationData compensation);
        Compensation GetById(string id);
    }
}
