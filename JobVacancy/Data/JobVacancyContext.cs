using Microsoft.EntityFrameworkCore;

namespace JobVacancy.Data
{
    public class JobVacancyContext : DbContext
    {
        public JobVacancyContext(DbContextOptions<JobVacancyContext> options) : base(options)
        {

        }

        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Location> Location { get; set; }

    }

   
}
