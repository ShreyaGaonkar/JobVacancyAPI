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
    public class LocationController : ControllerBase
    {
        private JobVacancyContext _dbContext;
        public LocationController(JobVacancyContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// This GET method returns all the available locations
        /// </summary>
        [HttpGet("Locations")]
        public IActionResult GetDepartments()
        {
            try
            {
                var locations = _dbContext.Location.ToList();
                if (locations.Count == 0)
                {
                    return StatusCode(404, "No location found");
                }
                return Ok(locations);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occurred");
            }

        }

        /// <summary>
        /// This POST method creates locations
        /// </summary>
        [HttpPost("Locations")]
        public IActionResult CreateDepartments([FromBody] LocationRequest request)
        {
            Location locations = new Location();
            locations.Title = request.Title;
            locations.City = request.City;
            locations.State = request.State;
            locations.Country = request.Country;
            locations.Zip = request.Zip;

            try
            {
                _dbContext.Location.Add(locations);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error has occurred");
            }
            return Ok();
        }

        /// <summary>
        /// This PUT method updates specific location based on Id
        /// </summary>
        [HttpPut("Locations")]
        public IActionResult UpdateDepartments([FromBody] LocationRequest request)
        {
            try
            {
                var locations = _dbContext.Location.FirstOrDefault(x => x.Id == request.Id);
                if (locations == null)
                {
                    return StatusCode(404, "Location not found");
                }

                locations.Title = request.Title;
                locations.City = request.City;
                locations.State = request.State;
                locations.Country = request.Country;
                locations.Zip = request.Zip;

                _dbContext.Entry(locations).State = EntityState.Modified;
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
