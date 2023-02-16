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
    public class UserDto :IMapFrom<User>
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Email { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
