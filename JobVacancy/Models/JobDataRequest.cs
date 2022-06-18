using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobVacancy.Models
{
    public class JobDataRequest
    {
        /// <summary>
        /// Search string based on job title
        /// </summary>
        public string q { get; set; }

        /// <summary>
        /// Page Number
        /// </summary>

        public int pageNo { get; set; }

        /// <summary>
        /// Page Size
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// Job Location Id
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// Job Department Id
        /// </summary>
        public int DepartmentId { get; set; }
    }
}
