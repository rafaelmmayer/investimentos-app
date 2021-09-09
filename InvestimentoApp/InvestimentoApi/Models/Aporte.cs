using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestimentoApi.Models
{
    public class Aporte
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoEnvio { get; set; }
        public Ativo Ativo { get; set; }

        public Aporte()
        {

        }

        // Constructor for DTO
        public Aporte(int quantidade, decimal precoEnvio, int ativoId)
        {
            Quantidade = quantidade;
            PrecoEnvio = precoEnvio;
            Ativo = new Ativo() { Id = ativoId };
        }
    }
}
