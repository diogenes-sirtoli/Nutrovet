using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;

namespace Nutrovet
{
	public partial class PortalNutrovet : System.Web.UI.Page
	{
		protected clPortalContatosBll contatosBll = new clPortalContatosBll();
		protected PortalContato contatosDcl;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				//string senha = Funcoes.Funcoes.Seguranca.Descriptografar("tXBi+9sRXIg=");
				//lblAno.Text = senha;

				alertas.Visible = false;
				lblAno.Text = DateTime.Today.ToString("yyyy");
			}
		}

		protected void lbEmail_Click(object sender, EventArgs e)
		{
			contatosDcl = new PortalContato();

			contatosDcl.Assunto = tbxAssunto.Text;
			contatosDcl.NomeContato = tbxName.Text;
			contatosDcl.EmailContato = tbxEmail.Text;
			contatosDcl.Mensagem = tbxMsg.Text;
			contatosDcl.DataEnvio = DateTime.Today;
			contatosDcl.DataResposta = DateTime.Parse("01/01/1910");
			contatosDcl.MsgSituacao = (int)DominiosBll.PortalContatoAuxSituacao.Enviada;

			bllRetorno _ret = contatosBll.Inserir(contatosDcl);

			if (_ret.retorno)
			{
				LimpaTela();
				EnviarEmailAdm(contatosDcl);

				alertas.Attributes["class"] = "alert alert-success alert-dismissible";
				lblAlerta.Text = @"
					Sua Mensagem Foi Enviada com Sucesso. <br/> 
					Em Breve lhe Enviaremos a Resposta !";
				alertas.Visible = true;
			}
			else
			{
				alertas.Attributes["class"] = "alert alert-danger alert-dismissible";
				lblAlerta.Text = _ret.mensagem;
				alertas.Visible = true;
			}
		}

		private void EnviarEmailAdm(PortalContato _contato)
		{
			Funcoes.Funcoes.fncRetorno fncRetornoBll = new
					Funcoes.Funcoes.fncRetorno();

			Funcoes.Funcoes.EMail.EmailDe = _contato.EmailContato;
			Funcoes.Funcoes.EMail.EmailPara = "contato@nutrovet.com.br";
			Funcoes.Funcoes.EMail.Mensagem = _contato.Mensagem;
			Funcoes.Funcoes.EMail.Assunto = _contato.Assunto;

			Funcoes.Funcoes.EMail.SMTP = "dedrelay.secureserver.net";
			Funcoes.Funcoes.EMail.Porta = 25;
			Funcoes.Funcoes.EMail.Conta = "contato@nutrovet.com.br";
			Funcoes.Funcoes.EMail.Senha = "m@r5ped9";
			Funcoes.Funcoes.EMail.SSL = false;

			fncRetornoBll = Funcoes.Funcoes.EMail.Enviar();
		}

		private void LimpaTela()
		{
			tbxAssunto.Text = "";
			tbxName.Text = "";
			tbxEmail.Text = "";
			tbxMsg.Text = "";
		}
	}
}