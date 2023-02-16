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
        public Position PostitionDepart { get; set; }
        public Position PostitionArrival { get; set; }
        public DateTime DepartTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public List<Position>? Steps { get; set; }

        public void Mapping(Profile profile) => profile.CreateMap<CreateTrajetRequestDto, Trajet>().ReverseMap();
    }
}
