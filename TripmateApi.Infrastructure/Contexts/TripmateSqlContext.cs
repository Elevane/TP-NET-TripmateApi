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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user.HasIndex(u => u.Id);
            });


            base.OnModelCreating(builder);
        }


        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
