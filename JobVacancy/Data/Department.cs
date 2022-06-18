using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobVacancy.Data
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
