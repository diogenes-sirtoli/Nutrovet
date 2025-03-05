using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;

namespace Nutrovet.Plano
{
    public partial class EscolherAssinatura : System.Web.UI.Page
    {
        protected clAcessosVigenciaCupomBll cupomBll = new clAcessosVigenciaCupomBll();
        protected AcessosVigenciaCupomDesconto cupomDcl = null;
        protected TOPessoasBll pessoaTO;
        protected TOPlanosBll planosTO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rblBasico.SelectedIndex = -1;
                rblIntermediario.SelectedIndex = -1;
                rblCompleto.SelectedIndex = -1;
                rblProntuario.SelectedIndex = -1;
                rblReceituario.SelectedIndex = -1;
            }
        }

        protected void btnAvancar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Plano/ResumoAssinatura.aspx");
        }

        protected void rblBasico_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblIntermediario.SelectedIndex = -1;
            rblCompleto.SelectedIndex = -1;
        }

        protected void rblIntermediario_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblBasico.SelectedIndex = -1;
            rblCompleto.SelectedIndex = -1;
        }

        protected void rblCompleto_SelectedIndexChanged(object sender, EventArgs e)
        {
            rblIntermediario.SelectedIndex = -1;
            rblBasico.SelectedIndex = -1;
        }
    }
}