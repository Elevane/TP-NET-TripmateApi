using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Infrastructure.Contexts;
using TripmateApi.Infrastructure.Contexts.Interfaces;

namespace TripmateApi.Infrastructure.Repositories.Trajets
{
    public class TrajetsRepository
    {
        public ITripmateContext Context;

        public TrajetsRepository(ITripmateContext context)  => Context = context;



    }
}
