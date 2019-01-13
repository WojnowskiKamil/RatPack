using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rat_Pack.Models.Database;
using System.Data.Entity;

namespace Rat_Pack.Context
{
    public class DataContext : IdentityDbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Truck> Trucks { get; set; }


        public DataContext() : base("DatabaseContext")
        {
        }
    }
}