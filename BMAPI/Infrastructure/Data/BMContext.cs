using BMAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMAPI.Infrastructure.Data
{
    public class BMContext : DbContext
    {
        public BMContext(DbContextOptions<BMContext> options)
          : base(options)
        {

        }
        public DbSet<TravelRoute> TravelRoutes { get; set; }
    }
}
