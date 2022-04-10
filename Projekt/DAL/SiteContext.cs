using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Projekt.DAL
{
    public class SiteContext : DbContext
    {
        public SiteContext() : base("DefaultConnection") { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Profil> Profiles { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Reservation> Reservations { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
          //  modelBuilder.Entity<Reservation>().HasRequired<Trip>(r => r.Trip)
            //    .WithMany(t => t.Reservations).HasForeignKey(r => r.TripId);

        }

    }
}