using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestimentoApi.DTOs
{
    public class AporteDTO
    {
        public int Quantidade { get; set; }
        public decimal PrecoEnvio { get; set; }
        public int AtivoId { get; set; }
    }
}
