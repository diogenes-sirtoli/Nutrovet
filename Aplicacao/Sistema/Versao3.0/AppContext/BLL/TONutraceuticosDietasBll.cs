using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TONutraceuticosDietasBll
    {
        public int IdNutDie { get; set; }
        public int? EmReceita { get; set; }
        public int IdEspecie { get; set; }
        public string Especie { get; set; }
        public int IdNutr { get; set; }
        public string Nutriente { get; set; }
        public int IdNutrac { get; set; }
        public int IdDieta { get; set; }
        public string Dieta { get; set; }
        public decimal? DoseMin { get; set; }
        public int? IdUnidMin { get; set; }
        public string UnidadeMin { get; set; }
        public decimal? DoseMax { get; set; }
        public int? IdUnidMax { get; set; }
        public string UnidadeMax { get; set; }
        public decimal? Dose { get; set; }
        public int? IdUnid { get; set; }
        public string Unidade { get; set; }
        public int? IdPrescr { get; set; }
        public string Prescricao { get; set; }
        public int? IdPrescr1 { get; set; }
        public string Prescricao1 { get; set; }
        public int? IdPrescr2 { get; set; }
        public string Prescricao2 { get; set; }
        public decimal? PesoAtual { get; set; }
        public decimal? Quantidade { get; set; }
        public string Observacao { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
