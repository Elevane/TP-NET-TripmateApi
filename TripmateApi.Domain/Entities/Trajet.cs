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
                if(Steps.Any(s => trajet.Steps.Any(d => d.PostitionArrival.City == s.PostitionArrival.City) &&
                  trajet.Steps.Any(d => d.PostitionDepart.City == s.PostitionDepart.City) &&
                  trajet.Steps.Any(d => d.DepartTime == s.DepartTime)))
                        return true;       
            }
            return false;
        }
        
    }
}
