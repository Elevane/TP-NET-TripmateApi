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
            Trajet exist = await _context.Trajets.FirstOrDefaultAsync(trajet => trajet.DriverId == driverId && trajet.PostitionDepart.City == dto.PostitionDepart.City && trajet.PostitionDepart.City == dto.PostitionDepart.City && trajet.DepartTime  == dto.DepartTime);
            if (exist != null)
                return Result.Failure("User already has trajet with same departure & and arrival point on the same date");

            Trajet toCreate = _mapper.Map<Trajet>(dto);
            User driver = await _context.Users.FirstOrDefaultAsync(user => user.Id == driverId);
            toCreate.DriverId = driverId;
            toCreate.Driver = driver;

            await _context.Trajets.AddAsync(toCreate);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
