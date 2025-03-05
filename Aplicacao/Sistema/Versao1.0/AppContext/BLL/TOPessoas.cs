using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    [Serializable]
    public class TOPessoasBll
    {
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public int? IdTpPessoa { get; set; }
        public string TipoPessoa { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public int? CRVM { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Logotipo { get; set; }
        public int? IdCliente { get; set; }
        public string Cliente { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public bool? Bloqueado { get; set; }
        public bool? NewUser { get; set; }
        public bool? PlanoVencido { get; set; }
        public int? NrCupom { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
