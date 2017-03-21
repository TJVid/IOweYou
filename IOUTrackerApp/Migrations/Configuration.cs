namespace IOUTrackerApp.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IOUTrackerApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(IOUTrackerApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            if (context.IOUStatus.Count() == 0)
            {
                var colorRed = new Color("Red");
                var statusBorrowed = new IOUStatus();
                statusBorrowed.status = "Borrowed";
                statusBorrowed.color = colorRed;
                context.IOUStatus.Add(statusBorrowed);
                context.SaveChanges();

                var colorGreen = new Color("Green");
                var statusReturned = new IOUStatus();
                statusReturned.color = colorGreen;
                statusReturned.status = "Returned";
                context.IOUStatus.Add(statusReturned);
                context.SaveChanges();

                if (context.Colors.Count() <= 2)
                {
                    List<string> colorList = new List<string> { "Yellow", "Pink", "Brown", "Blue", "Purple" };
                    foreach (var c in colorList)
                    {
                        context.Colors.Add(new Color(c));
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
