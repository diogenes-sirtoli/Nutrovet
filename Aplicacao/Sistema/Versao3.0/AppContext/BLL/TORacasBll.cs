using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TORacasBll
    {
        public int IdEspecie { get; set; }
        public string Especie { get; set; }
        public int IdRaca { get; set; }
        public string Raca { get; set; }
        public int? IdadeAdulta { get; set; }
        public int? CrescInicial { get; set; }
        public int? CrescFinal { get; set; }

        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
