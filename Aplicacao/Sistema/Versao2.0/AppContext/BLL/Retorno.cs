using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class bllRetorno
    {
        public bool retorno { get; set; }
        public string mensagem { get; set; }
        public List<object> objeto { get; set; }

        public static bllRetorno GeraRetorno(bool ret, string mensagem)
        {
            bllRetorno r = new bllRetorno();

            r.mensagem = mensagem;
            r.retorno = ret;

            return r;
        }

        public static bllRetorno GeraRetorno(bool ret, string mensagem, List<object> objeto)
        {
            bllRetorno r = new bllRetorno();

            r.mensagem = mensagem;
            r.retorno = ret;
            r.objeto = objeto;

            return r;
        }
    }
}
