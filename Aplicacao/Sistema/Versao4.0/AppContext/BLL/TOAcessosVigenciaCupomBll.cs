using System;

namespace BLL
{
    public class TOAcessosVigenciaCupomBll
    {
        public int IdCupom { get; set; }
        public string NrCupom { get; set; }
        public int? dPlanoTp { get; set; }
        public string TipoPlano { get; set; }
        public DateTime DtInicial { get; set; }
        public DateTime DtFinal { get; set; }
        public string Professor { get; set; }
        public bool? fUsado { get; set; }
        public bool? fAcessoLiberado { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
