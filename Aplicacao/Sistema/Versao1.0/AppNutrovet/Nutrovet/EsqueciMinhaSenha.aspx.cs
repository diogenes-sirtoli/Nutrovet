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
    public partial class EsqueciMinhaSenha : System.Web.UI.Page
    {
        protected Pessoa pessoaDcl;
        protected clPessoasBll pessoaBll = new clPessoasBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                alertas.Visible = false;

                txbEmail.Focus();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string _mensagem = "";

            if (txbEmail.Text != "")
            {
                if (ValidaEMail(txbEmail.Text))
                {
                    alertas.Visible = false;
                    pessoaDcl = pessoaBll.CarregarLogon(txbEmail.Text);

                    _mensagem = "A Senha do USUÁRIO " + txbEmail.Text + " é: " +
                        Funcoes.Funcoes.Seguranca.Descriptografar(pessoaDcl.Senha);

                    Funcoes.Funcoes.fncRetorno fncRetornoBll = EnviarEmail(
                        txbEmail.Text, _mensagem);

                    if (fncRetornoBll.retorno)
                    {
                        alertas.Attributes["class"] =
                            "alert alert-success alert-dismissible";
                        lblAlerta.Text = @"
                            E-Mail enviado com sucesso!";
                        alertas.Visible = true;
                    }
                    else
                    {
                        alertas.Attributes["class"] =
                            "alert alert-danger alert-dismissible";
                        lblAlerta.Text = @"
                            Não foi possível enviar o E-Mail! <br/> 
                            Verifique o texto digitado!";
                        alertas.Visible = true;
                    }
                }
                else
                {
                    alertas.Attributes["class"] = "alert alert-danger alert-dismissible";
                    lblAlerta.Text = @"E-Mail não Cadastrado !";
                    alertas.Visible = true;
                }
            }
            else
            {
                alertas.Attributes["class"] = "alert alert-info alert-dismissible";
                lblAlerta.Text = @"Você Precisa Informar um E-Mail !";
                alertas.Visible = true;
            }
        }

        private bool ValidaEMail(string text)
        {
            bool _retorno = pessoaBll.ExisteUsuario(text).retorno;

            return _retorno;
        }

        private Funcoes.Funcoes.fncRetorno EnviarEmail(string _eMail, string _msg)
        {
            Funcoes.Funcoes.fncRetorno fncRetornoBll = new
                    Funcoes.Funcoes.fncRetorno();

            Funcoes.Funcoes.EMail.EmailDe = "contato@nutrovet.com.br";
            Funcoes.Funcoes.EMail.EmailPara = _eMail;
            Funcoes.Funcoes.EMail.Mensagem = _msg;
            Funcoes.Funcoes.EMail.Assunto = "Central de Serviço";

            Funcoes.Funcoes.EMail.SMTP = "dedrelay.secureserver.net";
            Funcoes.Funcoes.EMail.Porta = 25;
            Funcoes.Funcoes.EMail.Conta = "contato@nutrovet.com.br";
            Funcoes.Funcoes.EMail.Senha = "m@r5ped9";
            Funcoes.Funcoes.EMail.SSL = false;

            //Funcoes.Funcoes.EMail.SMTP = "smtpout.secureserver.net";
            //Funcoes.Funcoes.EMail.Porta = 465;
            //Funcoes.Funcoes.EMail.Conta = "contato@nutrovet.com.br";
            //Funcoes.Funcoes.EMail.Senha = "m@r5ped9";
            //Funcoes.Funcoes.EMail.SSL = false;

            fncRetornoBll = Funcoes.Funcoes.EMail.Enviar();

            return fncRetornoBll;
        }
    }
}