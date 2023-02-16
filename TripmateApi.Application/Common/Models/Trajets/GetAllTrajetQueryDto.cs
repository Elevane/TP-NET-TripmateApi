using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripmateApi.Application.Common.Models.Trajets
{
    public class GetAllTrajetQueryDto
    {
        public PositionDto? PositionDepart { get; set; }
        public PositionDto? PositionArrival { get; set; }
        public int? MinDuration { get; set; }
        public int? MaxDuration { get; set; }
        public DateTime? MinDepartTime { get; set; }
        public DateTime? MaxDepartTime { get; set; }
    }
}
