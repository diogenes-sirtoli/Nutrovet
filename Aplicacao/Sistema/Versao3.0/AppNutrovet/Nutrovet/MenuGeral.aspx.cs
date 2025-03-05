using System;
using System.Web.Security;
using System.Web.UI;
using DCL;
using BLL;

namespace Nutrovet
{
    public partial class MenuGeral : Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clPessoasBll pessoaBll = new clPessoasBll();
        protected Pessoa pessoaDcl;
        protected clAcessosVigenciaPlanosBll planosBll = new clAcessosVigenciaPlanosBll();
        protected AcessosVigenciaPlano planoDcl;
        protected TOAcessosVigenciaPlanosBll planoTO;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    lblAno.Text = DateTime.Today.ToString("yyyy");
                    int _idPessoa = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);

                    acessosDcl = acessosBll.Carregar(_idPessoa);

                    PopulaDashBoard(_idPessoa);
                    RemoveSessions();

                    if (acessosDcl != null)
                    {
                        if (!Funcoes.Funcoes.ConvertePara.Bool(acessosDcl.TermoUso))
                        {
                            mdlTermoUso.Show();
                        }

                        if (Funcoes.Funcoes.ConvertePara.Bool(acessosDcl.SuperUser))
                        {
                            Session["Inserir"] = true;
                            Session["Alterar"] = true;
                            Session["Excluir"] = true;
                            Session["Consultar"] = true;
                            Session["Relatorios"] = true;
                            Session["SuperUser"] = true;
                            Session["AcaoEsp"] = true;
                        }
                        else
                        {
                            if (planosBll.PlanoEstaNaVigencia(_idPessoa) && 
                                pessoaBll.CamposPrincipaisCadastrados(_idPessoa))
                            {
                                Session["Inserir"] = acessosDcl.Inserir;
                                Session["Alterar"] = acessosDcl.Alterar;
                                Session["Excluir"] = acessosDcl.Excluir;
                                Session["Consultar"] = acessosDcl.Consultar;
                                Session["Relatorios"] = acessosDcl.Relatorios;
                                Session["SuperUser"] = acessosDcl.SuperUser;
                                Session["AcaoEsp"] = acessosDcl.AcoesEspeciais;

                                if ((Request.QueryString["perm"] != null) &&
                                    (Funcoes.Funcoes.Seguranca.Descriptografar(
                                        Funcoes.Funcoes.ConvertePara.String(
                                        Request.QueryString["perm"])) == "False"))
                                {
                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Info,
                                        "Você Não Tem Permissão para</br>Acessar Esta Página",
                                        "NutroVet Informa",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                }
                                else if ((Request.QueryString["receit"] != null) &&
                                         (Funcoes.Funcoes.Seguranca.Descriptografar(
                                            Funcoes.Funcoes.ConvertePara.String(
                                            Request.QueryString["receit"])) == "False"))
                                {
                                    Funcoes.Funcoes.Toastr.ShowToast(this,
                                        Funcoes.Funcoes.Toastr.ToastType.Info,
                                        "</br>Somente Plano COMPLETO Possuí Acesso a " +
                                        "esta Funcionalidade!</br>Faça um Upgrade de Planos na Tela" +
                                        " do seu Perfil!",
                                        "NutroVET Informa - Receituário",
                                        Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                                        true);
                                }
                            }
                            else
                            {
                                Response.Redirect("~/Perfil/Perfil.aspx");
                            }
                        }
                    }
                    else
                    {
                        FormsAuthentication.RedirectToLoginPage("perm=" +
                            Funcoes.Funcoes.Seguranca.Criptografar("False"));
                    }
                }
            }
        }

        private void PopulaDashBoard(int _idCliente)
        {
            TOSistemaDWHBll resumo = acessosBll.ResumoSistema(_idCliente);

            lblTotalAlimentos.Text = Funcoes.Funcoes.ConvertePara.String(
                resumo.TotalAlimentos);
            lblTotalCardapios.Text = Funcoes.Funcoes.ConvertePara.String(
                resumo.TotalCardapios);
            lblTotalPacientes.Text = Funcoes.Funcoes.ConvertePara.String(
                resumo.TotalPacientes);
            lblTotalTutores.Text = Funcoes.Funcoes.ConvertePara.String(
                resumo.TotalTutores);
        }

        private void RemoveSessions()
        {
            Session.Remove("alimentosIndicados");
            Session.Remove("alimentosContra");
            Session.Remove("ToastrDietas");
            Session.Remove("ToastrCardapios");
            Session.Remove("dadosPaciente");
            Session.Remove("cardAlimTotais");
            Session.Remove("ToastrNutraceuticos");
            Session.Remove("ToastrPacientes");
            Session.Remove("ToastrTutores");
            Session.Remove("ToastrAlimentos");
            Session.Remove("ToastrMensagens");
            Session.Remove("ToastrAssinantes");
            Session.Remove("ToastrLogs");
            Session.Remove("especieSelecao");
            Session.Remove("BalancoDieta");
            Session.Remove("ExigenciasNutricionais");
            Session.Remove("PlanoAssinatura");
            Session.Remove("DadosComprador");
            Session.Remove("Receituario");
        }

        protected void btnOcultarTermoUso_Click(object sender, EventArgs e)
        {
            acessosDcl = acessosBll.Carregar(
                Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));

            acessosDcl.TermoUso = true;
            acessosDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(
                User.Identity.Name);
            acessosDcl.Ativo = true;
            acessosDcl.DataCadastro = DateTime.Now;
            acessosDcl.IP = Request.UserHostAddress;

            bllRetorno retAlteracao = acessosBll.Alterar(acessosDcl);

        }
    }
}