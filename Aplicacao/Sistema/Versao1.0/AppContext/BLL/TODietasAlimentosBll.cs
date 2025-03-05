using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TODietasAlimentosBll
    {
        public int IdDieta { get; set; }
        public string Dieta { get; set; }
        public int IdDietaAlim { get; set; }
        public int IdAlimento { get; set; }
        public string Alimento { get; set; }
        public int? IdTpIndicacao { get; set; }
        public string TipoIndicacao { get; set; }
        public decimal? Quant { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
