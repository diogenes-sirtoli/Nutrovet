using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TOAcessosVigenciaPlanosBll
    {
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public int IdVigencia { get; set; }
        public int IdPlano { get; set; }
        public string Plano { get; set; }
        public DateTime DtInicial { get; set; }
        public DateTime DtFinal { get; set; }
        public int IdCupom { get; set; }
        public string NrCupom { get; set; }
        public int PercentDesconto { get; set; }
        public DateTime ValidadeCupomInicial { get; set; }
        public DateTime ValidadeCupomFinal { get; set; }
        public int? QtdAnim { get; set; }
        public int? Periodo { get; set; }
        public string PeriodoDescr { get; set; }
        public bool? Receituario { get; set; }
        public bool? Prontuario { get; set; }
        public bool? ComprovanteAnexou { get; set; }
        public string ComprovantePath { get; set; }
        public bool? ComprovanteHomologado { get; set; }
        public DateTime? ComprovanteDtHomolog { get; set; }
        public int? ComprovanteHomologador { get; set; }
        public string ComprovanteHomologadorNome { get; set; }
        public string Situacao { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
