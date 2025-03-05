using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;
using System.Globalization;

namespace Nutrovet.Temp
{
    public partial class Teste : System.Web.UI.Page
    {
        protected clNutraceuticosDietasBll nutraDieBll = new clNutraceuticosDietasBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            //double _teste1 = 595.250;
            //string _teste2 = string.Format(CultureInfo.InvariantCulture, 
            //    "{0}", _teste1);
            //string _test3 = _teste2.Replace(",", ".");


            //GerarSenha();
            //Descriptografar();
        }

        private void Descriptografar()
        {
            Label1.Text = "Cartão 1: " + Funcoes.Funcoes.Seguranca.Descriptografar(
                "hAoUYMSIFiHTxlC4ClVnVlHDUqLL4cCd");
            Label2.Text = "Cartão 2: " + Funcoes.Funcoes.Seguranca.Descriptografar(
                "uRURRZh/5biIVZw5aHWOcqG8hJ1VnQdC");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            clAnimaisAuxEspeciesBll especiesBll = new clAnimaisAuxEspeciesBll();
            string strConex = "Data Source=SICORP-01\\SQLEXPRESS;Initial Catalog=Nutrovet;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=A1x291x0;MultipleActiveResultSets=True;TrustServerCertificate=True;Packet Size=4096";
            var listagem = especiesBll.Listar("", 10, 1);
            var listagem2 = especiesBll.Listar("", 10, 1, TextBox1.Text);

            dgPivot.DataSource= listagem2;
            dgPivot.DataBind();
        }

        protected void GerarSenha()
        {
            string _senha = Funcoes.Funcoes.Seguranca.GerarChaveCripto();
            string _senha2 = Funcoes.Funcoes.Seguranca.GerarChaveCripto();

            string _crypto = Funcoes.Funcoes.Seguranca.Criptografar("Rafael Britto Lobo", 
                _senha);
            string _deCrypto = Funcoes.Funcoes.Seguranca.Descriptografar(_crypto,
                _senha);

            string _deCrypto2 = Funcoes.Funcoes.Seguranca.Descriptografar(_crypto,
                _senha2);
        }
    }
}