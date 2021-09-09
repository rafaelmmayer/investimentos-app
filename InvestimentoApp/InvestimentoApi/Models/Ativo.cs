using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestimentoApi.Models
{
    public class Ativo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public Classe Classe { get; set; }

        public Ativo()
        {

        }

        // Contructor for DTO
        public Ativo(string codigo, string descricao, int classeId)
        {
            Codigo = codigo;
            Descricao = descricao;
            Classe = new Classe() { Id = classeId };
        }
    }
}
