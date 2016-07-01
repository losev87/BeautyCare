using System.Data.Entity;
using BeautyCare.Model.Entity;

namespace BeautyCare.Context
{
    public class BeautyCareContext : DbContext
    {
        public BeautyCareContext()
            : base("BeautyCareContext")
        {
        }

        public DbSet<Publication> Publications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}
