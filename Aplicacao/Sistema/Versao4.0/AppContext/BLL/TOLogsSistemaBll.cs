using System;

namespace BLL
{
    public class TOLogsSistemaBll
    {
        public int IdPessoa { get; set; }
        public string Assinante { get; set; }
        public int IdLog { get; set; }
        public int? IdTabela { get; set; }
        public string Tabela { get; set; }
        public int? IdAcao { get; set; }
        public string Acao { get; set; }
        public string Mensagem { get; set; }
        public DateTime? Datahora { get; set; }
    }
}
