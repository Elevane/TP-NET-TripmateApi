using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Application.Common.Mapping;
using TripmateApi.Application.Common.Models.Authentification.interfaces;
using TripmateApi.Entities.Entities;

namespace TripmateApi.Application.Common.Models.Authentification
{
    public class InternalAuthenticatedUserDto : IMapFrom<User>, IInternalUser
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InternalAuthenticatedUserDto, User>().ReverseMap();
        }
    }
}

