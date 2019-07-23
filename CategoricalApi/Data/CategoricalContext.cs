using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace CategoricalApi.Data
{
    public class CategoricalContext : DbContext
    {
        public CategoricalContext(DbContextOptions<CategoricalContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}