using System;

namespace BLL
{
    public class TONutraceuticosBll
    {
        public int IdNutrac { get; set; }
        public int IdEspecie { get; set; }
        public string Especie { get; set; }
        public int IdGrupo { get; set; }
        public string Grupo { get; set; }
        public int IdNutr { get; set; }
        public string Nutriente { get; set; }
        public decimal? DoseMin { get; set; }
        public int? IdUnidMin { get; set; }
        public string UnidadeMin { get; set; }
        public decimal? DoseMax { get; set; }
        public int? IdUnidMax { get; set; }
        public string UnidadeMax { get; set; }
        public int? IdPrescr1 { get; set; }
        public string TpPrescr1 { get; set; }
        public int? IdPrescr2 { get; set; }
        public string TpPrescr2 { get; set; }
        public string Obs { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}