using JobVacancy.Data;
using JobVacancy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JobVacancy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private JobVacancyContext _dbContext;
        public DepartmentController(JobVacancyContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// This GET method returns all the available departments
        /// </summary>
        [HttpGet("Departments")]
        public IActionResult GetDepartments()
        {
            try
            {
                var departments = _dbContext.Department.ToList();
                if (departments.Count == 0)
                {
                    return StatusCode(404, "No department found");
                }
                return Ok(departments);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occurred");
            }

        }

        /// <summary>
        /// This POST method creates departments
        /// </summary>
        [HttpPost("Departments")]
        public IActionResult CreateDepartments([FromBody] DepartmentRequest request)
        {
            Department departments = new Department();
            departments.Title = request.Title;

            try
            {
                _dbContext.Department.Add(departments);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            return Ok();
        }

        /// <summary>
        /// This PUT method updates specific department based on Id
        /// </summary>
        [HttpPut("Departments")]
        public IActionResult UpdateDepartments([FromBody] DepartmentRequest request)
        {
            try
            {
                var departments = _dbContext.Department.FirstOrDefault(x => x.Id == request.Id);
                if (departments == null)
                {
                    return StatusCode(404, "Department not found");
                }

                departments.Title = request.Title;

                _dbContext.Entry(departments).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            return Ok();
        }
    }
}
