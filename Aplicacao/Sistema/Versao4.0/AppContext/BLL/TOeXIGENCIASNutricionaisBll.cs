using System;

namespace BLL
{
    public class TOExigenciasNutricionaisBll
    {
        public int IdExigNutr { get; set; }
        public int IdTabNutr { get; set; }
        public string TabelaNutricional { get; set; }
        public int IdEspecie { get; set; }
        public string Especie { get; set; }
        public int IdIndic { get; set; }
        public string Indicacao { get; set; }
        public int? IdTpValor { get; set; }
        public string TipoValor { get; set; }
        public int IdNutr1 { get; set; }
        public string Nutriente1 { get; set; }
        public int? IdUnidade1 { get; set; }
        public string Unidade1 { get; set; }
        public decimal? Proporcao1 { get; set; }
        public decimal? Valor1 { get; set; }
        public int? IdNutr2 { get; set; }
        public string Nutriente2 { get; set; }
        public int? IdUnidade2 { get; set; }
        public string Unidade2 { get; set; }
        public decimal? Proporcao2 { get; set; }
        public decimal? Valor2 { get; set; }
        public decimal? TotalProporcao { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}