using System;

namespace BLL
{
    public class TOExigNutrTabelasBll
    {
        public int? IdGrupo { get; set; }
        public string Grupo { get; set; }
        public int? IdNutr { get; set; }
        public string Nutriente { get; set; }
        public int? IdUnid { get; set; }
        public string Unidade { get; set; }
        public int? IdTpValor { get; set; }
        public string TipoValor { get; set; }
        public decimal? Minimo { get; set; }
        public decimal? Maximo { get; set; }
        public decimal? Adequado { get; set; }
        public decimal? Recomendado { get; set; }
        public decimal? EmCardapio { get; set; }
        public decimal? Falta { get; set; }
        public decimal? Sobra { get; set; }
        public decimal? Valor { get; set; }
        public decimal? ValorCalc { get; set; }

        public TOExigNutrTabelasBll()
        {
            Minimo = 0;
            Maximo = 0;
            Adequado = 0;
            Recomendado = 0;
            EmCardapio = 0;
            Falta = 0;
            Sobra = 0;
            Valor = 0;
            ValorCalc = 0;
        }
    }
}
