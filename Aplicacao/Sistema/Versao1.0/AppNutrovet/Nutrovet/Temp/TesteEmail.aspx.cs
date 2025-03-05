using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nutrovet.Temp
{
    public partial class TesteEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblMsgTexto.Text = "";
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            Funcoes.Funcoes.fncRetorno fncRetornoBll = new
                    Funcoes.Funcoes.fncRetorno();

            Funcoes.Funcoes.EMail.EmailDe = tbDe.Text;
            Funcoes.Funcoes.EMail.EmailPara = tbPara.Text;//"contato@nutrovet.com.br";
            Funcoes.Funcoes.EMail.Mensagem = tbMsg.Text;
            Funcoes.Funcoes.EMail.Assunto = tbAssunto.Text;

            Funcoes.Funcoes.EMail.SMTP = tbSmtp.Text;
            Funcoes.Funcoes.EMail.Porta = Funcoes.Funcoes.ConvertePara.Int(
                tbPorta.Text);
            Funcoes.Funcoes.EMail.Conta = tbConta.Text;
            Funcoes.Funcoes.EMail.Senha = tbSenha.Text; //"m@r5ped9";
            Funcoes.Funcoes.EMail.SSL = cbxSSL.Checked;

            fncRetornoBll = Funcoes.Funcoes.EMail.Enviar();

            lblMsgTexto.Text = fncRetornoBll.mensagem;
        }
    }
}