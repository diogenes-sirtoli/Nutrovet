using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TOAtendimentoBll
    {
        public int IdCliente { get; set; }
        public string cNome { get; set; }
        public string cEMail { get; set; }
        public int IdTutor { get; set; }
        public string tNome { get; set; }
        public string tEMail { get; set; }
        public int IdAnimal { get; set; }
        public string Paciente { get; set; }
        public int? IdEspecie { get; set; }
        public string Especie { get; set; }
        public int? IdRaca { get; set; }
        public string Raca { get; set; }
        public int IdAtend { get; set; }
        public int IdTpAtend { get; set; }
        public string TipoAtendimento { get; set; }
        public string Descricao { get; set; }
        public DateTime? DtHrAtend { get; set; }
        public DateTime? DtAtend { get; set; }
        public DateTime? HrAtend { get; set; }
        public string Atendimento { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
