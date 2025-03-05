using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;
using System.Web.Security;

namespace Nutrovet
{
    public partial class Login : System.Web.UI.Page
    {
        protected clAcessosBll logonBll = new clAcessosBll();
        protected Acesso logonDcl = new Acesso();
        protected clPessoasBll pessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                alertas.Visible = false;

                if ((Request.QueryString["perm"] != null) &&
                    (Funcoes.Funcoes.Seguranca.Descriptografar(
                     Funcoes.Funcoes.ConvertePara.String(
                     Request.QueryString["perm"])) == "False"))
                {
                    Funcoes.Funcoes.Toastr.ShowToast(this,
                        Funcoes.Funcoes.Toastr.ToastType.Info,
                        "Você NÃO tem PERMISSÕES de ACESSO ! <br/> " + 
                        " Contate o Administrador", "Aviso",
                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                        true);
                }

                ViewState["count"] = 0;
                Page.Form.DefaultFocus = tbUser.ClientID;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bllRetorno ret;
            pessoaDcl = new Pessoa();

            pessoaDcl.Email = tbUser.Text;
            pessoaDcl.Senha = tbSenha.Text;

            ret = pessoaDcl.ValidarLogon();

            if (ret.retorno)
            {
                ViewState["count"] = 0;
                pessoaDcl = pessoaBll.CarregarLogon(tbUser.Text);

                //if (RegistraSessao(pessoaDcl))
                //{
                    Session["_dadosBasicos"] = pessoaDcl;

                    FormsAuthentication.RedirectFromLoginPage(
                        pessoaDcl.IdPessoa.ToString(), false);
                //}
                //else
                //{
                //    alertas.Attributes["class"] = "alert alert-danger alert-dismissible";
                //    lblAlerta.Text = @"
                //            Você está CONECTADO em Outro Dispositivo ! <br/> 
                //            Encerre sua Sessão para Continuar!";
                //    alertas.Visible = true;
                //}
            }
            else
            {
                if (ret.mensagem == "Usuário ou Senha Inválidos!")
                {
                    ViewState["count"] = Convert.ToInt32(ViewState["count"]) + 1;

                    //if (Convert.ToInt32(ViewState["count"]) == 3)
                    //{
                    //    ret = pessoaBll.Bloquear(tbUser.Text);

                    //    lblAlerta.Text = ret.mensagem;
                    //    alertas.Visible = true;
                    //}
                    //else
                    //{
                        alertas.Attributes["class"] =
                            "alert alert-danger alert-dismissible";
                    //lblAlerta.Text = @"Usuário ou Senha inválidos! <br/> 
                    //    Tentativa de acesso número <b>" + ViewState["count"].ToString() + "</b>";
                    lblAlerta.Text = @"Usuário ou Senha inválidos!";
                    alertas.Visible = true;

                        tbSenha.Focus();
                    //}
                }
                else
                {
                    alertas.Attributes["class"] = "alert alert-warning alert-dismissible";
                    lblAlerta.Text = ret.mensagem;
                    alertas.Visible = true;

                }
            }
        }

        private bool RegistraSessao(Pessoa pessoaDcl)
        {
            List<string> d = (List<string>)Application["UsersLoggedIn"];

            if (d != null)
            {
                lock (d)
                {
                    if (d.Contains(pessoaDcl.Email))
                    {
                        Session.Remove("UserLoggedIn");

                        return false;
                    }

                    d.Add(pessoaDcl.Email);
                }
            }

            Session["UserLoggedIn"] = pessoaDcl.Email;

            return true;
        }
    }


}