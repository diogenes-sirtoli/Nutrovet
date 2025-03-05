using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nutrovet.Plano
{
    public partial class ResumoAssinatura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Plano/EscolherAssinatura.aspx");
        }

        protected void lbAvancarFinalizarAssinatura_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Plano/FinalizarAssinatura.aspx");
        }

        protected void rbPessoaFisica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPessoaFisica.Checked)
            {
                rbPessoaJuridica.Checked = false;
                lbNomeAssinante.Text = "Nome";
                lbTituloTipoPessoa.Text = "CPF";
                tbCNPJAssinante.Visible = false;
                tbCPFAssinante.Visible = true;
                divDataNascimento.Visible = true;
                divSobrenomeAssinante.Visible = true;
            }
        }

        protected void rbPessoaJuridica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPessoaJuridica.Checked)
            {
                rbPessoaFisica.Checked = false;
                lbNomeAssinante.Text = "Razão Social";
                lbTituloTipoPessoa.Text = "CNPJ";
                tbCNPJAssinante.Visible = true;
                tbCPFAssinante.Visible = false;
                divDataNascimento.Visible = false;
                divSobrenomeAssinante.Visible = false;
            }
        }
    }
}