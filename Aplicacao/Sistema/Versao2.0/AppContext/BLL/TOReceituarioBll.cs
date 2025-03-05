using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    [Serializable]
    public class TOReceituarioBll
    {
        public int IdTutor { get; set; }
        public string Tutor { get; set; }
        public int IdAnimal { get; set; }
        public string Animal { get; set; }
        public int? IdEspecie { get; set; }
        public string Especie { get; set; }
        public int? IdRaca { get; set; }
        public string Raca { get; set; }
        public int IdCardapio { get; set; }
        public string Descricao { get; set; }
        public DateTime? DtCardapio { get; set; }

        public int IdReceita { get; set; }
        public int dTpRec { get; set; }
        public string TipoReceita { get; set; }
        public string Arquivo { get; set; }
        public string NrReceita { get; set; }
        public string InstrucoesReceita { get; set; }
        public string Titulo { get; set; }        
        public string Veiculo { get; set; }
        public string QuantVeic { get; set; }
        public string Posologia { get; set; }
        public string Prescricao { get; set; }
        public DateTime? DataReceita { get; set; }
        public string LocalReceita { get; set; }

        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
