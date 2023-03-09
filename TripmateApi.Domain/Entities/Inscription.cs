using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Entities.Entities;

namespace TripmateApi.Domain.Entities
{
    public class Inscription
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Trajet Trajet { get; set; }
        public List<Step> Steps { get; set; }


        private Inscription() { }

        public Inscription (User user, Trajet trajet, List<Step> steps)
        {
            User = user; Trajet = trajet; Steps = steps;
        }
    }
}
