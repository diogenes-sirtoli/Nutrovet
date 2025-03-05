using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    [Serializable]
    public class TOTutoresBll
    {
        public int IdCliente { get; set; }
        public string Cliente { get; set; }
        public string cEmail { get; set; }
        public int IdTutores { get; set; }
        public int IdTutor { get; set; }
        public int? dTpEntidade { get; set; }
        public string TipoEntidade { get; set; }
        public string Tutor { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string Passaporte { get; set; }
        public string DocumentosOutros { get; set; }
        public string tEmail { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
