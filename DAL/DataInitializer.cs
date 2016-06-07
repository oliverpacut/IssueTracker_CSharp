using System.Data.Entity;

namespace DAL
{
    class DataInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
