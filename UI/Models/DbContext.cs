using System.Data.Entity;

namespace UI.Models
{
    public class TrainerContext : DbContext
    {
        public TrainerContext()
        {
            Database.SetInitializer<TrainerContext>(new DropCreateDatabaseIfModelChanges<TrainerContext>());
        }

        public DbSet<EntityModel> Vocabulary { get; set; }
    }
}
