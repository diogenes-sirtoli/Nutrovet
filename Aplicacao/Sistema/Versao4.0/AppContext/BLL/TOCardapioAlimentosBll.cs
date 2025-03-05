using System;

namespace BLL
{
    public class TOCardapioAlimentosBll
    {
        public int IdCardapio { get; set; }
        public int IdCardapAlim { get; set; }
        public int IdAlimento { get; set; }
        public string Alimento { get; set; }
        public string AlimentoFonte { get; set; }
        public string AlimentoTexto { get; set; }
        public string AlimentoPesquisa { get; set; }
        public decimal? Quant { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
