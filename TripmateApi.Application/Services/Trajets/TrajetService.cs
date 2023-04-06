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
        
        public async Task<Result> Delete(int trajetId, int driverId)
        {
            Trajet exist = await _context.Trajets.FirstOrDefaultAsync(t => t.Id == trajetId);
            if(exist == null)
                return Result.Failure<List<GetAllTrajetResponseDto>>("No matching trajet was found with this Id.");
            if(exist.DriverId != driverId)
                return Result.Failure<List<GetAllTrajetResponseDto>>("You are not allowed to delete this trajet.");
            _context.Steps.RemoveRange(_context.Steps.Where(s => s.TrajetId == trajetId));
            _context.Trajets.Remove(exist);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        public async Task<Result<List<GetAllTrajetResponseDto>>> FindAllUser(int driverId)
        {
            List<Trajet> trajets = await _context.Trajets.Where(
                trajet => 
                trajet.DriverId == driverId && 
                trajet.Steps.Any(step=> 
                    step.DepartTime.AddSeconds(Convert.ToDouble(step.Duration)) > DateTime.UtcNow)
                )
                .Include(t => t.Steps)
                .ThenInclude(s => s.PositionDepart)
                .Include(t => t.Steps)
                .ThenInclude(s => s.PositionArrival)
                .ToListAsync();
            List<GetAllTrajetResponseDto> dtos = _mapper.Map<List<GetAllTrajetResponseDto>>(trajets);
            return Result.Success(dtos);
        }
        public async Task<Result<List<GetAllTrajetResponseDto>>> FindAll(GetAllTrajetQueryDto query, int userId)
        {
            List<Trajet> trajets = null;
            if(query.PositionDepart != null)
                trajets = await _context.Trajets.Where(trajet =>
                trajet.Steps.Any(step => step.PositionDepart.Address == query.PositionDepart.Address) &&
                trajet.Steps.Any(step => step.PositionDepart.City == query.PositionDepart.City) &&
                trajet.Steps.Any(step => step.PositionDepart.Pc == query.PositionDepart.Pc) 
                ).Include((Trajet t) => t.Steps).ThenInclude((Step s) => s.PositionDepart).Include((Trajet t) => t.Steps).ThenInclude((Step s) => s.PositionArrival).ToListAsync();
            if (query.PositionArrival != null)
                trajets.Select( trajet =>
                trajet.Steps.Any(step => step.PositionArrival.Address == query.PositionArrival.Address) &&
                trajet.Steps.Any(step => step.PositionArrival.City == query.PositionArrival.City) &&
                trajet.Steps.Any(step => step.PositionArrival.Pc == query.PositionArrival.Pc)
                );
            if (query.MinDuration != null && query.MaxDuration != null)
                trajets.Select(trajet => trajet.Steps.Any(step => step.Duration < query.MaxDuration && step.Duration > query.MinDuration));
            if(query.MinDuration != null && query.MaxDuration == null)
                trajets.Select(trajet => trajet.Steps.Any(step => step.Duration > query.MinDuration));
            if (query.MinDuration == null && query.MaxDuration != null)
                trajets.Select(trajet => trajet.Steps.Any(step => step.Duration < query.MaxDuration));

            if (query.MinDepartTime != null && query.MaxDepartTime != null)
                trajets.Select(trajet => trajet.Steps.Any(step => step.DepartTime < query.MaxDepartTime && step.DepartTime > query.MinDepartTime));
            if (query.MinDepartTime != null && query.MaxDepartTime == null)
                trajets.Select(trajet => trajet.Steps.Any(step => step.DepartTime > query.MinDepartTime));
            if (query.MinDepartTime == null && query.MaxDepartTime != null)
                trajets.Select(trajet => trajet.Steps.Any(step => step.DepartTime < query.MaxDepartTime));
            if (trajets == null)
                return Result.Failure<List<GetAllTrajetResponseDto>>("No matching trajet was found with this query");
            trajets = trajets.Where(t => t.DriverId != userId).ToList();
            List<GetAllTrajetResponseDto> dtos = _mapper.Map<List<GetAllTrajetResponseDto>>(trajets);
            return Result.Success(dtos);
        }

        public async Task<Result> Validate(int inscriptionId, int userId)
        {
            Inscription exist = await _context.Inscriptions.Where(i => i.Id == inscriptionId).Include(i => i.Steps).Include(i => i.Trajet).FirstOrDefaultAsync();
            if (exist == null) return Result.Failure("Inscriptions you are trying to validate doesn't exist");
            if (exist.User == null || exist.Trajet == null || exist.Steps == null) return Result.Failure("The Inscription you want to validate has either no driver nor trajet nor steps.");
            Trajet trajet = await _context.Trajets.Where(t => t.Id == exist.Trajet.Id).Include(t => t.Steps).ThenInclude(s => s.Passangers).FirstOrDefaultAsync();
            if (trajet == null) return Result.Failure("trajet of the inscription doesn't exist");
            if (!trajet.HasRoom(exist.Steps.Select(s => s.Id).ToList())) return Result.Failure("Trajet has not enough room anymore");
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return Result.Failure("User trying to validate to trajet doesn't exist.");
            trajet.AddPassengerToSteps(user, exist.Steps);
            trajet.RemoveSteats(exist.Steps);

            exist.validate = true;
            _context.Inscriptions.Update(exist);
            _context.Trajets.Update(trajet);

            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> Update(UpdateTrajetRequestDto dto, int driverId)
        {
            
            Trajet exist = await _context.Trajets.Where(trajet =>
            trajet.DriverId == driverId && dto.Id == trajet.Id).Include(t => t.Steps).ThenInclude(s => s.PositionDepart).Include(t => t.Steps).ThenInclude(s => s.PositionArrival).FirstOrDefaultAsync();
            if (exist == null)
                return Result.Failure("Trajet you want to update doesn't exist");

            Trajet newValues = _mapper.Map<Trajet>(dto);
            exist.Steps = newValues.Steps;
          
             _context.Trajets.Update(exist);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        public async Task<Result> Create(CreateTrajetRequestDto dto, int driverId)
        {
            List<Trajet> trajets = await _context.Trajets.Where(trajet =>
            trajet.DriverId == driverId).Include((Trajet t) => t.Steps).ThenInclude((Step s) => s.PositionDepart).Include((Trajet t) => t.Steps).ThenInclude((Step s) => s.PositionArrival).ToListAsync();
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
    
        public async Task<Result> Inscription(InscriptionTrajetRequestDto request, int userId)
        {
            Trajet exist =await  _context.Trajets.Where(t => t.Id == request.TrajetId).Include(t => t.Steps).FirstOrDefaultAsync();
            if (exist == null)
                return Result.Failure("Trajet you trying to join doesn't exist.");
            if(!exist.HasSteps(request.Steps))
                return Result.Failure("The trajet you trying to join has not the steps you are giving.");
            if(!exist.HasRoom(request.Steps))
                return Result.Failure("Trajet you trying to join has no room left for the steps selected.");

            User passenger = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (passenger == null)
                return Result.Failure("Passenger doesn't exist");
            List<Step> passengerSteps = exist.GetPassengerSteps(request.Steps);
            Inscription toCreate = new Inscription(passenger, exist, passengerSteps);
            _context.Inscriptions.Add(toCreate);
            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
