using System;

namespace BLL
{
    public class TOAlimentoNutrientesBll
    {
        public int? IdAlimento { get; set; }
        public string Alimento { get; set; }
        public int? IdAlimNutr { get; set; }
        public int? IdGrupo { get; set; }
        public string Grupo { get; set; }
        public int? IdGrupoAlimento { get; set; }
        public string GrupoAlimento { get; set; }
        public int? IdCategoria { get; set; }
        public string Categoria { get; set; }
        public int? IdNutr { get; set; }
        public string Nutriente { get; set; }
        public int? IdFonte { get; set; }
        public string Fonte { get; set; }
        public int? Ordem { get; set; }
        public int? IdIndic { get; set; }
        public string Indicacao { get; set; }
        public string Referencia { get; set; }
        public decimal? Valor { get; set; }
        public decimal? ValMin { get; set; }
        public decimal? ValMax { get; set; }
        public decimal? Quant { get; set; }
        public decimal? ValCalc { get; set; }
        public int? IdUnidade { get; set; }
        public string Unidade { get; set; }
        public bool? ListarCardapio { get; set; }
        public int? OrdemTabCardapio { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
