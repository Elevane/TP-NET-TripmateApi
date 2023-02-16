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

        public User Driver { get; set; }
        public int DriverId { get; set; }

        public List<Step> Steps { get; set; }


        public bool HasSameTrajet(List<Trajet> listTrajets)
        {
            foreach (Trajet trajet in listTrajets) {
                if (trajet.Steps == null || trajet.Steps.Count < 1)
                    continue;
                if(Steps.Count > 0 && Steps.Any(s => trajet.Steps.Any(d => d?.PositionArrival?.City == s?.PositionArrival?.City) &&
                  trajet.Steps.Any(d => d?.PositionDepart?.City == s?.PositionDepart?.City) &&
                  trajet.Steps.Any(d => d.DepartTime.Hour == s.DepartTime.Hour && d.DepartTime.Day == s.DepartTime.Day && d.DepartTime.Month == d.DepartTime.Month && d.DepartTime.Year ==  d.DepartTime.Year)))
                        return true;       
            }
            return false;
        }
        
    }
}
