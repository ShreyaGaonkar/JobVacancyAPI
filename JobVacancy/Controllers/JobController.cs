using JobVacancy.Data;
using JobVacancy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;

namespace JobVacancy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private JobVacancyContext _dbContext;
        public JobController(JobVacancyContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// This POST method returns requested jobs
        /// </summary>
        [HttpPost("GetJobs")]
        public IActionResult GetJobs([FromBody] JobDataRequest request)
        {
            try
            {
                var getJobs = (from j in _dbContext.Jobs
                           join d in _dbContext.Department on j.DepartmentId equals d.Id
                           join l in _dbContext.Location on j.LocationId equals l.Id
                           where EF.Functions.Like(j.Title, "%" + request.q + "%")
                           select new
                           {
                               Id = j.Id,
                               Code = j.Code,
                               Title = j.Title,
                               DepartmentId = j.DepartmentId,
                               LocationId = j.LocationId,
                               Department = d.Title,
                               Location = l.Title,
                               PostedDate = j.PostedDate,
                               ClosingDate = j.ClosingDate
                           });

                if (request.DepartmentId !=0)
                {
                    getJobs = getJobs.Where(x => x.DepartmentId == request.DepartmentId);
                }
                if (request.LocationId != 0)
                {
                    getJobs = getJobs.Where(x => x.LocationId == request.LocationId);
                }

                var job = getJobs.Select(u => new
                {
                    Id = u.Id,
                    Code = u.Code,
                    Title = u.Title,
                    Department = u.Title,
                    Location = u.Title,
                    PostedDate = u.PostedDate,
                    ClosingDate = u.ClosingDate
                }).ToList();

                dynamic Jobs = new ExpandoObject();
                Jobs.Total = job.Count();
                Jobs.Data = job;

                if (job.Count == 0)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(Jobs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }

        }

        /// <summary>
        /// This GET method returns job based on specific Id
        /// </summary>
        [HttpGet("Jobs/{Id}")]
        public IActionResult GetSpecificJob([FromRoute] int Id)
        {
            try
            {
                var job = (from j in _dbContext.Jobs
                           where j.Id == Id
                           select new
                           {
                               Id = j.Id,
                               Code = j.Code,
                               Title = j.Title,
                               Description = j.Description,
                               PostedDate = j.ClosingDate,
                               ClosingDate = j.ClosingDate,
                               Department = (from d in _dbContext.Department where d.Id == j.DepartmentId select d).FirstOrDefault(),
                               Location = (from l in _dbContext.Location where l.Id == j.LocationId select l).FirstOrDefault()
                           }).FirstOrDefault();

                if (job == null)
                {
                    return StatusCode(404, "User not found");
                }
                return Ok(job);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            
        }

        /// <summary>
        /// This POST method create jobs
        /// </summary>
        [HttpPost("Jobs")]
        public IActionResult CreateJobs([FromBody] JobsRequest request)
        {

            int recentJobId = _dbContext.Jobs.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();

            Jobs job = new Jobs();
            job.Title = request.Title;
            job.Code = (Enumerable.Range(0, 9).Contains(recentJobId)) == true?"JOB-0"+ (recentJobId+1) : "JOB-" + (recentJobId + 1);
            job.Description = request.Description;
            job.DepartmentId = request.DepartmentId;
            job.LocationId = request.LocationId;
            job.ClosingDate = request.ClosingDate;
            job.PostedDate = DateTime.Now;

            try
            {
                var department = _dbContext.Department.FirstOrDefault(x => x.Id == request.DepartmentId);
                var location = _dbContext.Location.FirstOrDefault(x => x.Id == request.LocationId);
                if (department== null)
                {
                    return StatusCode(404, "Department does not exist");
                }
                if (location == null)
                {
                    return StatusCode(404, "Location does not exist");
                }

                _dbContext.Jobs.Add(job);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            return Ok();
        }

        /// <summary>
        /// This PUT method updates specific job based on Id
        /// </summary>
        [HttpPut("Jobs")]
        public IActionResult UpdateJobs([FromBody] JobsRequest request)
        {
            try
            {
                var job = _dbContext.Jobs.FirstOrDefault(x =>x.Id == request.Id);
                var department = _dbContext.Department.FirstOrDefault(x => x.Id == request.DepartmentId);
                var location = _dbContext.Location.FirstOrDefault(x => x.Id == request.LocationId);

                if (job == null)
                {
                    return StatusCode(404, "User not found");
                }
                if (department == null)
                {
                    return StatusCode(404, "Department does not exist");
                }
                if (location == null)
                {
                    return StatusCode(404, "Location does not exist");
                }

                job.Title = request.Title;
                job.Description = request.Description;
                job.DepartmentId = request.DepartmentId;
                job.LocationId = request.LocationId;
                job.ClosingDate = request.ClosingDate;

                _dbContext.Entry(job).State = EntityState.Modified;
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
