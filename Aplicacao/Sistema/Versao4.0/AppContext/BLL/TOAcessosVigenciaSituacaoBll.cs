using System;

namespace BLL
{
    public class TOAcessosVigenciaSituacaoBll
    {
        public int IdVigencia { get; set; }
        public int IdVigSit { get; set; }
        public int IdSituacao { get; set; }
        public string Situacao { get; set; }
        public DateTime? DataSituacao { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
