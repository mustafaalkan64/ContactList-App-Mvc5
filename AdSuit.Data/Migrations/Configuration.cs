namespace AdSuit.Data.Migrations
{
    using AdSuit.DAL.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AdSuit.Data.DAL.AdSuitDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AdSuit.Data.DAL.AdSuitDbContext context)
        {
            context.Tags.AddOrUpdate(
              new Tags { TagName = "Business" },
              new Tags { TagName = "Economy" },
              new Tags { TagName = "Finance" },
              new Tags { TagName = "Software" },
              new Tags { TagName = "Techonology" },
              new Tags { TagName = "Apple" },
              new Tags { TagName = "Microsoft" }

            );

            context.ContactTypes.AddOrUpdate(
              new ContactTypes { ContactType = "Phone Number", Name= "PhoneNumber" },
              new ContactTypes { ContactType = "Phone Number 2", Name = "PhoneNumber2" },
              new ContactTypes { ContactType = "Address", Name = "Address" },
              new ContactTypes { ContactType = "Email", Name = "Email" },
              new ContactTypes { ContactType = "Facebook Account Link", Name = "Facebook" },
              new ContactTypes { ContactType = "Twitter Account Link", Name = "Twitter" },
              new ContactTypes { ContactType = "Note", Name = "Note" }
            );

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
        }
    }
}
