namespace Rat_Pack.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Rat_Pack.Context.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Rat_Pack.Context.DataContext context)
        {
            var roleAdmin = context.Roles.SingleOrDefault(p => p.Name == "Klient");
            if (roleAdmin == null)
            {
                roleAdmin = new IdentityRole("Klient");
                context.Roles.Add(roleAdmin);
            }
            var roleAdmin1 = context.Roles.SingleOrDefault(p => p.Name == "Kierowca");
            if (roleAdmin1 == null)
            {
                roleAdmin1 = new IdentityRole("Kierowca");
                context.Roles.Add(roleAdmin1);
            }
            var roleAdmin2 = context.Roles.SingleOrDefault(p => p.Name == "Admin");
            if (roleAdmin2 == null)
            {
                roleAdmin2 = new IdentityRole("Admin");
                context.Roles.Add(roleAdmin2);
            }
        }
    }
}
