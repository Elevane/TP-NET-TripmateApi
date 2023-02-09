using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Entities.Entities;
using TripmateApi.Infrastructure.Contexts.Interfaces;

namespace TripmateApi.Infrastructure.Contexts
{
    public class TripMateSqlContext : DbContext, ITripmateContext
    {
        public TripMateSqlContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(user =>
            {
                user.HasIndex(u => u.Id);
            });


            base.OnModelCreating(builder);
        }
    }
}
