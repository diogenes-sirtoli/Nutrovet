using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    [Serializable]
    public class TOPlanosBll
    {
        public int IdVigencia { get; set; }
        public bool Escolheu { get; set; }
        public int? IdPlano { get; set; }
        public int? IdPlanoPagarMe { get; set; }
        public int? IdPlanoPagarMeTestes { get; set; }
        public string IdSubscriptionPagarMe { get; set; }
        public string StatusPagarMe { get; set; }
        public int? dNomePlano { get; set; }
        public string Plano { get; set; }
        public string PlanoComposto { get; set; }
        public int? dPlanoTp { get; set; }
        public string TipoPlano { get; set; }
        public int? dPeriodo { get; set; }
        public string Periodo { get; set; }
        public DateTime? DataPlanoInicial { get; set; }
        public DateTime? DataPlanoFinal { get; set; }
        public int? QtdAnimais { get; set; }
        public char AnualMensal { get; set; }
        public decimal? ValorPlano { get; set; }
        public decimal? ValorTotal { get; set; }
        public string ValorDescricao { get; set; }
        public string TextoProduto { get; set; }
        public int VoucherId { get; set; }
        public bool VoucherUsado { get; set; }
        public string VoucherNr { get; set; }
        public int VoucherTipo { get; set; }
        public decimal? VoucherValor { get; set; }
        public bool fAcessoLiberado { get; set; }
        public bool EstaNaVigencia { get; set; }
        public bool? Ativo { get; set; }
        public int? IdOperador { get; set; }
        public string IP { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
