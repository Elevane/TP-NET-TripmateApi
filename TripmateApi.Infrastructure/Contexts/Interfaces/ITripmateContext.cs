using Microsoft.EntityFrameworkCore;
using TripmateApi.Entities.Entities;

namespace TripmateApi.Infrastructure.Contexts.Interfaces
{
    public interface ITripmateContext
    {
        DbSet<User> Users { get; set; }
    }
}
