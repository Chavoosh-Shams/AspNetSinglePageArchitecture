using Microsoft.EntityFrameworkCore;
using SinglePageArchitecture.Models.DomainModels.PersonAggregates;

namespace SinglePageArchitecture.Models
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Person> Person {  get; set; }
    }
}
