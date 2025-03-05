using System;

namespace BLL
{
    public class TOFuncoesTelas
    {
        public int IdFuncTela { get; set; }
        public int IdAcFunc { get; set; }
        public string Funcao { get; set; }
        public int IdTela { get; set; }
        public string Telas { get; set; }
        public string CodTela { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
