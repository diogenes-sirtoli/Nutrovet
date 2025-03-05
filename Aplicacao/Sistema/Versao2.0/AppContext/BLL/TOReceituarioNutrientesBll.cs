using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    [Serializable]
    public class TOReceituarioNutrientesBll
    {
        public int IdReceita { get; set; }
        public int dTpRec { get; set; }
        public string TipoReceita { get; set; }
        public string Titulo { get; set; }
        public string Arquivo { get; set; }
        public string NrReceita { get; set; }
        public DateTime? DataReceita { get; set; }
        public int IdNutrRec { get; set; }
        public bool? EmReceita { get; set; }
        public int IdNutr { get; set; }
        public string Nutriente { get; set; }
        public decimal? Consta { get; set; }
        public decimal? Falta { get; set; }
        public decimal? DoseMin { get; set; }
        public int? IdUnidMin { get; set; }
        public string UnidadeMin { get; set; }
        public int? IdPrescrMin { get; set; }
        public string PrescricaoMin { get; set; }
        public decimal? DoseMax { get; set; }
        public int? IdUnidMax { get; set; }
        public string UnidadeMax { get; set; }
        public int? IdPrescrMax { get; set; }
        public string PrescricaoMax { get; set; }
        public decimal? Dose { get; set; }
        public int? IdUnid { get; set; }
        public string Unidade { get; set; }
        public int? IdPrescr { get; set; }
        public string Prescricao { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
