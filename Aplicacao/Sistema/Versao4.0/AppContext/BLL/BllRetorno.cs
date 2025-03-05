using System.Collections.Generic;

namespace BLL
{
    public class bllRetorno
    {
        public bool Retorno { get; set; }
        public bool RegisReativ { get; set; }
        public string Mensagem { get; set; }
        public List<object> Objeto { get; set; }

        public static bllRetorno GeraRetorno(bool ret, string mensagem)
        {
            bllRetorno r = new bllRetorno
            {
                Mensagem = mensagem,
                Retorno = ret
            };

            return r;
        }

        public static bllRetorno GeraRetorno(bool ret, bool regisReativ, string mensagem)
        {
            bllRetorno r = new bllRetorno
            {
                Mensagem = mensagem,
                Retorno = ret,
                RegisReativ = regisReativ
            };

            return r;
        }

        public static bllRetorno GeraRetorno(bool ret, string mensagem, List<object> objeto)
        {
            bllRetorno r = new bllRetorno
            {
                Mensagem = mensagem,
                Retorno = ret,
                Objeto = objeto
            };

            return r;
        }

        public static bllRetorno GeraRetorno(bool ret, bool regisReativ,
            string mensagem, List<object> objeto)
        {
            bllRetorno r = new bllRetorno
            {
                Mensagem = mensagem,
                Retorno = ret,
                Objeto = objeto,
                RegisReativ = regisReativ
            };

            return r;
        }
    }
}
