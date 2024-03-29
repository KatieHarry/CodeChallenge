﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Models;
using CodeChallenge.Services.Employees;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);

            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }


        // Gets employee info, not including direct reports.
        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(string id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // Gets employee info, including direct reports tree and total.
        [HttpGet("getEmployeeReports/{id}", Name = "getEmployeeReports")]
        public IActionResult GetEmployeeReports(string id)
        {
            _logger.LogDebug($"Received employee reports get request for '{id}'");

            var reportingStructure = _employeeService.GetReports(id);

            if (reportingStructure == null)
                return NotFound();

            return Ok(reportingStructure);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(string id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }
    }
}
