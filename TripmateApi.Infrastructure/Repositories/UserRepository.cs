using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Entities.Entities;
using TripmateApi.Infrastructure.Contexts.Interfaces;

namespace TripmateApi.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly ITripmateContext _context;
        public UserRepository(ITripmateContext context) => _context = context;

        public async Task<User> FindOne(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.FirstOrDefaultAsync(predicate);
        }
    }
}
