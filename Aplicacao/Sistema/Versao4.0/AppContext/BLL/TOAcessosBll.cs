using System;

namespace BLL
{
    public class TOAcessosBll
    {
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Email { get; set; }
        public int? IdAcFunc { get; set; }
        public string Funcao { get; set; }
        public int? IdVigencia { get; set; }
        public DateTime? DtInicial { get; set; }
        public DateTime? DtFinal { get; set; }
        public int? IdPlano { get; set; }
        public string Plano { get; set; }
        public int? IdSituacao { get; set; }
        public string Situacao { get; set; }
        public int? QtdAnimais { get; set; }
        public int? Periodo { get; set; }
        public bool? Inserir { get; set; }
        public bool? Alterar { get; set; }
        public bool? Excluir { get; set; }
        public bool? Consultar { get; set; }
        public bool? AcoesEspeciais { get; set; }
        public bool? Relatorios { get; set; }
        public bool? SuperUser { get; set; }
    }
}
