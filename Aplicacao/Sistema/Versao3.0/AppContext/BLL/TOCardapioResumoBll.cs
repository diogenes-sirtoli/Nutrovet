using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable]
    public class TOCardapioResumoBll
    {
        public int IdAlimento { get; set; }
        public string Alimento { get; set; }
        public decimal? Quant { get; set; }
        public decimal? Carboidrato { get; set; }
        public decimal? CarboPerc { get; set; }
        public string CarboG { get; set; }
        public string CarboP { get; set; }
        public decimal? Proteina { get; set; }
        public decimal? ProtPerc { get; set; }
        public string ProtG { get; set; }
        public string ProtP { get; set; }
        public decimal? Gordura { get; set; }
        public decimal? GordPerc { get; set; }
        public string GordG { get; set; }
        public string GordP { get; set; }
        public decimal? Fibras { get; set; }
        public string FibrasG { get; set; }
        public string FibrasP { get; set; }
        public decimal? Energia { get; set; }
        public string EnergiaKcal { get; set; }
        public decimal? Umidade { get; set; }
        public string UmidageG { get; set; }
        public decimal? NEM { get; set; }

        public TOCardapioResumoBll()
        {
            IdAlimento = 0;
            Alimento = "";
            Quant = 0;
            Carboidrato = 0;
            Proteina = 0;
            Gordura = 0;
            Fibras = 0;
            Energia = 0;
            Umidade = 0;
            CarboPerc = 0;
            ProtPerc = 0;
            GordPerc = 0;
            Energia = 0;
            NEM = 0;
        }
    }
}
