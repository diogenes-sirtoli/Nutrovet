using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMeOld
{
    public class bllRetornoPMO
    {
        public bool retorno { get; set; }
        public string mensagem { get; set; }
        public List<object> objeto { get; set; }

        public static bllRetornoPMO GeraRetorno(bool ret, string mensagem)
        {
            bllRetornoPMO r = new bllRetornoPMO();

            r.mensagem = mensagem;
            r.retorno = ret;

            return r;
        }

        public static bllRetornoPMO GeraRetorno(bool ret, string mensagem, List<object> objeto)
        {
            bllRetornoPMO r = new bllRetornoPMO();

            r.mensagem = mensagem;
            r.retorno = ret;
            r.objeto = objeto;

            return r;
        }
    }
}
