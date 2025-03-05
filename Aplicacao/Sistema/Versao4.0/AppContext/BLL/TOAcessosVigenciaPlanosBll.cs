using System;

namespace BLL
{
    public class TOAcessosVigenciaPlanosBll
    {
        public int IdPessoa { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool? Bloqueado { get; set; }
        public int IdVigencia { get; set; }
        public int IdPlano { get; set; }
        public int? IdPlanoPagarMe { get; set; }
        public int? IdPlanoPagarMeTestes { get; set; }
        public string Plano { get; set; }
        public string IdSubscriptionPagarMe { get; set; }
        public string StatusPagarMe { get; set; }
        public decimal? ValorPlano { get; set; }
        public DateTime DtInicial { get; set; }
        public DateTime DtFinal { get; set; }
        public int? IdCartao { get; set; }
        public string NrCartao { get; set; }
        public string CodSeg { get; set; }
        public string VencimCartao { get; set; }
        public string NomeCartao { get; set; }
        public int? dBandeira { get; set; }
        public string Bandeira { get; set; }
        public int? dNomePlano { get; set; }
        public int? IdCupom { get; set; }
        public string NrCupom { get; set; }
        public DateTime? ValidadeCupomInicial { get; set; }
        public DateTime? ValidadeCupomFinal { get; set; }
        public bool? fAcessoLiberado { get; set; }
        public bool? AcessoNoSistema { get; set; }
        public int? QtdAnimais { get; set; }
        public int? dPeriodo { get; set; }
        public string Periodo { get; set; }
        public string Situacao { get; set; }
        public int? NaVigencia { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
