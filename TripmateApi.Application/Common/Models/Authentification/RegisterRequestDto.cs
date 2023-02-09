using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Application.Common.Mapping;
using TripmateApi.Entities.Entities;

namespace TripmateApi.Application.Common.Models.Authentification
{
    public class RegisterRequestDto : IMapFrom<User>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterRequestDto, User>().ReverseMap();
        }
    }
}
