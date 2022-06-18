using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobVacancy.Models
{
    public class JobsRequest
    {
        /// <summary>
        /// Job Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Job Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Job Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Job Department Id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Job Location Id
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// Job Closing Date
        /// </summary>
        public DateTime ClosingDate { get; set; }
    }
}
