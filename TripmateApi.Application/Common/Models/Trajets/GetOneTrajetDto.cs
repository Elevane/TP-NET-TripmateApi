using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Application.Common.Mapping;
using TripmateApi.Application.Common.Models.Authentification;
using TripmateApi.Domain.Entities;
using TripmateApi.Entities.Entities;

namespace TripmateApi.Application.Common.Models.Trajets
{
    public class GetOneTrajetDto : IMapFrom<Trajet>
    {
        public int Id {get; set;}
        public UserDto Driver { get; set; }
        public bool isSingleStep { get => Steps.Count > 1; }
        public List<StepDto> Steps { get; set; }

        public void Mapping(Profile profile) => profile.CreateMap<GetOneTrajetDto, Trajet>().ReverseMap();
    }

}
