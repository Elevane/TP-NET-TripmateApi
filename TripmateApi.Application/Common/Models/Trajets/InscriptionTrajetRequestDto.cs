using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripmateApi.Application.Common.Models.Trajets
{
    public class InscriptionTrajetRequestDto
    {
        public int TrajetId { get; set; }
        public List<int> Steps { get; set; }
    }
}
