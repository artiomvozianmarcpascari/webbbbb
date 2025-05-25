using System.Data.Entity.Migrations;

namespace Vintagefur.Infrastructure.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Vintagefur.Infrastructure.Data.VintagefurDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Vintagefur.Infrastructure.Data.VintagefurDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
