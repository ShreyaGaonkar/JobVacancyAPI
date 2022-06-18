using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobVacancy.Models
{
    public class DepartmentRequest
    {
        /// <summary>
        /// Department Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Job Department Title
        /// </summary>
        public string Title { get; set; }
    }
}
