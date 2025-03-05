using System;

namespace BLL
{
    public class TONutrientesBll
    {
        public int IdGrupo { get; set; }
        public string Grupo { get; set; }
        public int IdNutr { get; set; }
        public string Nutriente { get; set; }
        public int? Ordem { get; set; }
        public string Referencia { get; set; }
        public decimal? Valor { get; set; }
        public decimal? ValMin { get; set; }
        public decimal? ValMax { get; set; }
        public int? IdUnidade { get; set; }
        public string Unidade { get; set; }
        public bool? ListarCardapio { get; set; }
        public bool? ListarEmAlim { get; set; }
        public int? OrdemTabCardapio { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}