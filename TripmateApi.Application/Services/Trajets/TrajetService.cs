using AutoMapper;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Application.Common.Models.Trajets;
using TripmateApi.Infrastructure.Repositories.Trajets;

namespace TripmateApi.Application.Services.Trajets
{
    public class TrajetService 
    {
        private readonly TrajetsRepository _trajetsRepository;
        private readonly IMapper _mapper;

        public TrajetService(TrajetsRepository trajetsRepository, IMapper mapper)
        {
            _trajetsRepository= trajetsRepository;
            _mapper=mapper;
        }


        public async Task<Result> Create(CreateTrajetRequestDto dto)
        {
            return Result.Success();
        }
    }
}
