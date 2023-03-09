using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Domain.Entities;
using TripmateApi.Entities.Entities;
using TripmateApi.Infrastructure.Contexts.Interfaces;

namespace TripmateApi.Infrastructure.Contexts
{
    public class TripMateSqlContext : DbContext, ITripmateContext
    {
        public TripMateSqlContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
         public DbSet<Trajet> Trajets { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }

        public DbSet<Step> Steps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user.HasIndex(u => u.Id);
            });
            builder.Entity<Trajet>(trajet =>
            {
                trajet.HasIndex(t => t.Id);
            });
            builder.Entity<Step>(step =>
            {
                step.ToTable("step");
                step.HasIndex(s => s.Id);
                step.HasMany(s => s.Passangers).WithMany(u => u.Voyages);
                step.HasOne(s => s.PositionDepart).WithOne().HasForeignKey<Step>(s => s.PositionDepartId).OnDelete(DeleteBehavior.Cascade);
                step.HasOne(s => s.PositionArrival).WithOne().HasForeignKey<Step>(s => s.PositionArrivalId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Inscription>(i =>
            {
                i.ToTable("inscriptions");
                i.HasMany<Step>(ins => ins.Steps).WithMany(s => s.Inscriptions);
                i.HasOne<Trajet>(ins => ins.Trajet);
                i.HasOne<User>(ins => ins.User);
            });

            base.OnModelCreating(builder);
        }


        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
