using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobVacancy.Models
{
    public class LocationRequest
    {
        /// <summary>
        /// Job Location Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Job Location
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// City in which job is avaiable
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Job Id
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// State in which job is avaiable
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Country in which job is avaiable
        /// </summary>
        public int Zip { get; set; }

    }
}
