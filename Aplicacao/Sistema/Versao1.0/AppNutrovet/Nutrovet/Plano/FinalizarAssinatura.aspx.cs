using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nutrovet.Plano
{
    public partial class FinalizarAssinatura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Plano/ResumoAssinatura.aspx");
        }

        protected void lbAssinar_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Plano/FinalizarAssinatura.aspx");
        }

        protected void lbVerificarVoucher_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Plano/FinalizarAssinatura.aspx");
        }
        
    }
}