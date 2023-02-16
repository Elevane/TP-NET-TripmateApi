using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Entities.Entities;

namespace TripmateApi.Domain.Entities
{
    public class Trajet
    {
        public int Id { get; set; }
        public Position PostitionDepart { get; set; }
        public Position PostitionArrival { get; set; }
        public DateTime DepartTime { get; set; }
        public TimeSpan? Duration { get; set; }

        public User Driver { get; set; }
        public int DriverId { get; set; }
        public List<Position>? Steps { get; set; }
    }
}
