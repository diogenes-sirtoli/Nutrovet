using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TOAcessosVigenciaCupomBll
    {
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public int IdVigencia { get; set; }
        public int IdPlano { get; set; }
        public string Plano { get; set; }
        public int IdCupom { get; set; }
        public string NrCupom { get; set; }
        public int PercentDesconto { get; set; }
        public DateTime DtInicial { get; set; }
        public DateTime DtFinal { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
