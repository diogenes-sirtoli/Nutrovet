using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;


namespace Nutrovet.Temp
{
    public partial class Teste : System.Web.UI.Page
    {
        protected clTutoresBll tutoresBll = new clTutoresBll();
        protected clAssinaturaPMOBll assinaturaBll = new clAssinaturaPMOBll();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var _assinatura = assinaturaBll.Listar(5, 2);
            
            
            //TextBox2.Text = _assinatura.NameCustomer;

            //Plan _plano = pagarMeBll.Plano(_assinatura.Plan.Id, true);
            //TOAssinaturaPMOBll _assinaturaTO = new TOAssinaturaPMOBll();

            //_assinaturaTO.NameCustomer = TextBox2.Text;


            //assinaturaBll.Inserir(_assinaturaTO, true);

            //Funcoes.Funcoes.Toastr.ShowToast(this,
            //            Funcoes.Funcoes.Toastr.ToastType.Warning,
            //            "Esta é uma Conta Cliente !</br>Não É Possível Alterá-la!",
            //            "NutroVET informa - Alteração",
            //            Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
            //            true);
        }
    }
}