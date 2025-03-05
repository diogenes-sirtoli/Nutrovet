using System;
using System.Collections.Generic;
using System.Web.UI;
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

        protected clAcessosVigenciaPlanosBll acessosVigenciaPlanosBLL = new clAcessosVigenciaPlanosBll();
        protected AcessosVigenciaPlano acessosVigenciaPlanoDCL;
        protected clAssinaturaPMOBll assinaturaPMOBll = new clAssinaturaPMOBll();
        protected clLogsSistemaBll logsBll = new clLogsSistemaBll();
        protected LogsSistema logsDcl;

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

                VerificaConexao();
                ViewState["count"] = 0;
                Page.Form.DefaultFocus = tbUser.ClientID;
            }
        }

        private void VerificaConexao()
        {
            bllRetorno _conexao = pessoaBll.VerificaConexao();

            if (!_conexao.retorno)
            {
                alertas.Attributes["class"] = "alert alert-danger alert-dismissible";
                lblAlerta.Text = _conexao.mensagem;
                alertas.Visible = true;
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
                TOAcessosVigenciaPlanosBll planoTO = acessosVigenciaPlanosBLL.CarregarPlano(
                    pessoaDcl.IdPessoa);

                Session["_dadosBasicos"] = pessoaDcl;

                if (planoTO != null)
                {
                    if ((planoTO.IdSubscriptionPagarMe != null) &&
                        (Funcoes.Funcoes.ConvertePara.Int(planoTO.IdSubscriptionPagarMe) > 0))
                    {
                        bllRetorno _retAtualizaDatas = assinaturaPMOBll.AtualizaDatasAssinatura(
                            pessoaDcl.IdPessoa);
                    }

                    Session["_dadosBasicosAssinatura"] = planoTO.IdVigencia;
                }

                InserirLog(pessoaDcl);

                FormsAuthentication.RedirectFromLoginPage(pessoaDcl.IdPessoa.ToString(), false);

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

        private void InserirLog(Pessoa _pessoa)
        {
            logsDcl = new LogsSistema();

            logsDcl.IdPessoa = _pessoa.IdPessoa;
            logsDcl.IdTabela = (int)DominiosBll.LogTabelas.Pessoas;
            logsDcl.IdAcao = (int)DominiosBll.AcoesCrud.Efetuar_Logon;
            logsDcl.Mensagem = string.Format("{0} {1} Efetuou LOGON em {2}",
                Funcoes.Funcoes.CarregarEnumNome<DominiosBll.PessoasAuxTipos>(
                _pessoa.IdTpPessoa), _pessoa.Nome, DateTime.Today.ToString("dd/MM/yyyy"));
            logsDcl.Justificativa = "";
            logsDcl.Datahora = DateTime.Now;

            bllRetorno bllRetorno = logsBll.Inserir(logsDcl);
        }

        protected bllRetorno GravaDadosNoBanco(TOAcessosVigenciaPlanosBll _dadosAssinatura)
        {
            bllRetorno retornoDados = new bllRetorno();

            AcessosVigenciaPlano vigenciaPlanoDcl = new AcessosVigenciaPlano();

            acessosVigenciaPlanoDCL.DtInicial = _dadosAssinatura.DtInicial;
            acessosVigenciaPlanoDCL.DtFinal = _dadosAssinatura.DtFinal;

            pessoaDcl.AcessosVigenciaPlanos.Add(vigenciaPlanoDcl);

            retornoDados = pessoaBll.Inserir(pessoaDcl);

            return retornoDados;
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