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
    public class UpdateTrajetRequestDto : IMapFrom<Trajet>
    {
        public int Id { get; set; }

        public List<StepDto> Steps { get; set; }
    }
}
