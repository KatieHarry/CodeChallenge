﻿using CodeChallenge.Models;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories.Compensations
{
    public interface ICompensationRepository
    {
        Compensation GetById(string id);
        Compensation Add(Compensation compensation);
        Task SaveAsync();
    }
}