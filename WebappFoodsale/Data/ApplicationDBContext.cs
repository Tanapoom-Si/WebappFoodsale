using Microsoft.EntityFrameworkCore;
using WebappFoodsale.Models;

namespace WebappFoodsale.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        {

        }

        public DbSet<Food> Foods { get; set; }
    }
}
