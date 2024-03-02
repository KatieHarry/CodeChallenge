using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Models;
using CodeChallenge.Services.Compensations;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService)
        {
            _logger = logger;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] CompensationData compensation)
        {
            _logger.LogDebug($"Received employee create request for '{compensation.EmployeeId}'");

            var result = _compensationService.Create(compensation);

            return CreatedAtRoute("getCompensationById", new { id = result.Id }, result);
        }

        [HttpGet("{id}", Name = "getCompensationById")]
        public IActionResult GetCurrentCompensationById(string id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _compensationService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }
    }
}
