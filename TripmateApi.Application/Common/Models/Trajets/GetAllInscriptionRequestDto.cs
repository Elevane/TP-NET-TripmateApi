using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Domain.Entities;
using  TripmateApi.Application.Common.Models.Authentification;
using TripmateApi.Application.Common.Mapping;
using AutoMapper;

namespace TripmateApi.Application.Common.Models.Trajets
{
    public class GetAllInscriptionRequestDto : IMapFrom<Inscription>
    {
        public UserDto User { get; set; }
        public List<StepDto> Steps { get; set; }

        public bool validate { get; set; }

        public void Mapping(Profile profile) => profile.CreateMap<GetAllInscriptionRequestDto, Inscription>().ReverseMap();
    }
}
