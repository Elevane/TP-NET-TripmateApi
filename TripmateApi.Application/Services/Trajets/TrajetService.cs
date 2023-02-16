using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TripmateApi.Application.Common.Models.Trajets;
using TripmateApi.Domain.Entities;
using TripmateApi.Entities.Entities;
using TripmateApi.Infrastructure.Contexts.Interfaces;

namespace TripmateApi.Application.Services.Trajets
{
    public class TrajetService 
    {
        private readonly ITripmateContext _context;
        private readonly IMapper _mapper;

        public TrajetService(ITripmateContext context, IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
        }

        public async Task<Result> Create(CreateTrajetRequestDto dto, int driverId)
        {
            List<Trajet> trajets = await _context.Trajets.Where(trajet =>
            trajet.DriverId == driverId).Include(t => t.Steps).ThenInclude(s => s.PostitionDepart).Include(t => t.Steps).ThenInclude(s => s.PostitionArrival).ToListAsync();
            Trajet toCreate = _mapper.Map<Trajet>(dto);
            if(toCreate.HasSameTrajet(trajets))
                return Result.Failure("User already has trajet with same departure & and arrival point on the same date");    
            User driver = await _context.Users.FirstOrDefaultAsync(user => user.Id == driverId);
            toCreate.DriverId = driverId;
            toCreate.Driver = driver;

            await _context.Trajets.AddAsync(toCreate);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
