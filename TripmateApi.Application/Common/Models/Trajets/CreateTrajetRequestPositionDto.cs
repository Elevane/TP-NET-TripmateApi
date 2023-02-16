﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Application.Common.Mapping;
using TripmateApi.Domain.Entities;

namespace TripmateApi.Application.Common.Models.Trajets
{
    public class CreateTrajetRequestPositionDto : IMapFrom<Position>
    {
        public string City { get; set; }
        public string Address { get; set; }
        public int Pc { get; set; }
        public void Mapping(Profile profile) => profile.CreateMap<CreateTrajetRequestPositionDto, Position>().ReverseMap();
    }
}
