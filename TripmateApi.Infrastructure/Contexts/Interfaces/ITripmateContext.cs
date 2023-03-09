using Microsoft.EntityFrameworkCore;
using TripmateApi.Domain.Entities;
using TripmateApi.Entities.Entities;

namespace TripmateApi.Infrastructure.Contexts.Interfaces
{
    public interface ITripmateContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Trajet> Trajets { get; set; }
        DbSet<Step> Steps { get; set; }
        DbSet<Inscription> Inscriptions { get; set; }
        public Task<int> SaveChangesAsync();
    }
}
