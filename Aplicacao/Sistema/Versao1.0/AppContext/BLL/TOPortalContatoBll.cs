using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TOPortalContatoBll
    {
        public int IdContato { get; set; }
        public string NomeContato { get; set; }
        public string EmailContato { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public DateTime? DataEnvio { get; set; }
        public int? MsgSituacao { get; set; }
        public string Situacao { get; set; }
        public DateTime? DataResposta { get; set; }
        public string Observacoes { get; set; }
    }
}
