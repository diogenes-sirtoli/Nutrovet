using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BLL;

namespace Nutrovet
{
    public partial class MenuGeral : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected Pessoa pessoasDcl;
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

                    acessosDcl = acessosBll.Carregar(
                        Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));

                    PopulaDashBoard(Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name));
                    RemoveSessions();

                    if (acessosDcl != null)
                    {
                        if (!Funcoes.Funcoes.ConvertePara.Bool(acessosDcl.TermoUso))
                        {
                            mdlTermosUso.Show();
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
            Session.Remove("especieSelecao");
            Session.Remove("BalancoDieta");
            Session.Remove("ExigenciasNutricionais");
            Session.Remove("PlanoAssinatura");
            Session.Remove("DadosComprador");
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