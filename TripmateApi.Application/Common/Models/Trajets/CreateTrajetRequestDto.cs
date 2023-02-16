using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Application.Common.Mapping;
using TripmateApi.Domain.Entities;
using TripmateApi.Entities.Entities;

namespace TripmateApi.Application.Common.Models.Trajets
{
    public class CreateTrajetRequestDto : IMapFrom<Trajet>
    {
        public CreateTrajetRequestPositionDto PostitionDepart { get; set; }
        public CreateTrajetRequestPositionDto PostitionArrival { get; set; }
        public DateTime DepartTime { get; set; }
        public int? Duration { get; set; }
        public List<CreateTrajetRequestPositionDto>? Steps { get; set; }

        public void Mapping(Profile profile) => profile.CreateMap<CreateTrajetRequestDto, Trajet>().ReverseMap();
    }
}
