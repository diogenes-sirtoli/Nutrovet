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
        public string Plano { get; set; }
        public int? QtdAnimais { get; set; }
        public char AnualMensal { get; set; }
        public double ValorTotal { get; set; }
        public string TextoProduto { get; set; }
        public bool Receituario { get; set; }
        public bool Prontuario { get; set; }
        public bool VoucherUsado { get; set; }
        public string VoucherNr { get; set; }
        public int VoucherTipo { get; set; }
        public bool fAcessoLiberado { get; set; }
        public bool EstaNaVigencia { get; set; }
    }
}
