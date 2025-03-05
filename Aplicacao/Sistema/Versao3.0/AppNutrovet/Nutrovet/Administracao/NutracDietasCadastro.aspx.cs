using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DCL;
using BLL;
using Org.BouncyCastle.Ocsp;

namespace Nutrovet.Administracao
{
    public partial class NutracDietasCadastro : System.Web.UI.Page
    {
        protected clAcessosBll acessosBll = new clAcessosBll();
        protected Acesso acessosDcl;
        protected clNutrientesBll nutriBll = new clNutrientesBll();
        protected Nutriente nutriDcl;
        protected clDietasBll dietasBll = new clDietasBll();
        protected Dietas dietasDcl;
        protected clNutraceuticosDietasBll nutracDietasBll = new clNutraceuticosDietasBll();
        protected NutraceuticosDieta nutracDietasDcl;
        protected TONutraceuticosDietasBll nutracDietasTO;
        protected clNutraceuticosBll nutraceuticosBll = new clNutraceuticosBll();
        protected Nutraceutico nutraceuticosDcl;
        protected clAnimaisAuxEspeciesBll especiesBll = new clAnimaisAuxEspeciesBll();
        protected AnimaisAuxEspecy especiesDcl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (acessosBll.Permissao(
                    Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name), "1.4.4",
                    Funcoes.Funcoes.ConvertePara.Bool(Session["SuperUser"])))
                {
                    if (!Page.IsPostBack)
                    {
                        lblAno.Text = DateTime.Today.ToString("yyyy");

                        PopulaEspecie();
                    }
                }
                else
                {
                    Response.Redirect("~/MenuGeral.aspx?perm=" +
                        Funcoes.Funcoes.Seguranca.Criptografar("False"));
                }
            }
        }

        private void PopulaEspecie()
        {
            ddlEspecie.DataValueField = "Id";
            ddlEspecie.DataTextField = "Nome";
            ddlEspecie.DataSource = especiesBll.Listar();
            ddlEspecie.DataBind();

            ddlEspecie.Items.Insert(0, new ListItem("-- Selecione --", "0"));
            ddlNutrac.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaNutrac(int _idEspecie)
        {
            ddlNutrac.DataValueField = "IdNutrac";
            ddlNutrac.DataTextField = "Nutriente";
            ddlNutrac.DataSource = nutraceuticosBll.Listar(_idEspecie);
            ddlNutrac.DataBind();

            ddlNutrac.Items.Insert(0, new ListItem("-- Selecione --", "0"));
        }

        private void PopulaDietas(int _idEspecie, int _idNutrac)
        {
            lbxDietas.DataValueField = "IdDieta";
            lbxDietas.DataTextField = "Dieta";
            lbxDietas.DataSource = nutracDietasBll.ListarDietasNaoCadastradas(
                _idEspecie, _idNutrac);
            lbxDietas.DataBind();
        }

        private void PopulaDietasCadastradas(int _idEspecie, int _idNutrac)
        {
            lbxCadastro.DataValueField = "IdDieta";
            lbxCadastro.DataTextField = "Dieta";
            lbxCadastro.DataSource = nutracDietasBll.ListarDietasCadastradas(
                _idEspecie, _idNutrac);
            lbxCadastro.DataBind();
        }

        protected void ddlNutrac_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNutraceutico.Text = " - " + ddlNutrac.SelectedItem.Text;
            PopulaDietas(Funcoes.Funcoes.ConvertePara.Int(ddlEspecie.SelectedValue),
                Funcoes.Funcoes.ConvertePara.Int(ddlNutrac.SelectedValue));
            PopulaDietasCadastradas(Funcoes.Funcoes.ConvertePara.Int(ddlEspecie.SelectedValue),
                Funcoes.Funcoes.ConvertePara.Int(ddlNutrac.SelectedValue));
        }

        protected void btnVai_Click(object sender, EventArgs e)
        {
            if (Funcoes.Funcoes.ControlForm.ListBoxNrItensSelec(lbxDietas, true) > 0)
            {
                ListItemCollection lista = Funcoes.Funcoes.ControlForm.ListBoxSelecionados(
                    lbxDietas, true);

                foreach (ListItem item in lista)
                {
                    bllRetorno insert = Inserir(item);

                    if (insert.retorno)
                    {
                        lbxDietas.Items.Remove(item);
                    }
                }

                lbxCadastro = Funcoes.Funcoes.ControlForm.ListBoxOrdenaItens(
                    lbxCadastro);

                PopulaDietasCadastradas(Funcoes.Funcoes.ConvertePara.Int(ddlEspecie.SelectedValue),
                    Funcoes.Funcoes.ConvertePara.Int(ddlNutrac.SelectedValue));

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    "Iten(s) Cadastrado(s) com Sucesso!",
                    "NutroVET informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Não há itens selecionados!", 
                    "NutroVET informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void btnVem_Click(object sender, EventArgs e)
        {
            if (Funcoes.Funcoes.ControlForm.ListBoxNrItensSelec(lbxCadastro, true) > 0)
            {
                ListItemCollection lista = Funcoes.Funcoes.ControlForm.ListBoxSelecionados(
                    lbxCadastro, true);

                foreach (ListItem item in lista)
                {
                    bllRetorno excluir = Excluir(item);

                    if (excluir.retorno)
                    {
                        lbxCadastro.Items.Remove(item);
                        lbxDietas.Items.Add(item);
                    }
                }

                lbxDietas = Funcoes.Funcoes.ControlForm.ListBoxOrdenaItens(
                    lbxDietas);

                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Success,
                    "Iten(s) Descadastrado(s) com Sucesso!",
                    "NutroVET informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
            else
            {
                Funcoes.Funcoes.Toastr.ShowToast(this,
                    Funcoes.Funcoes.Toastr.ToastType.Warning,
                    "Não há itens selecionados!",
                    "NutroVET informa",
                    Funcoes.Funcoes.Toastr.ToastPosition.TopCenter,
                    true);
            }
        }

        protected void ddlEspecie_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaNutrac(Funcoes.Funcoes.ConvertePara.Int(ddlEspecie.SelectedValue));
            LimpaDietas();
            LimpaDietasCadastro();
        }

        private void LimpaDietas()
        {
            lbxDietas.Items.Clear();
            lbxDietas.DataSource = null;
            lbxDietas.DataBind();
        }

        private void LimpaDietasCadastro()
        {
            lbxCadastro.Items.Clear();
            lbxCadastro.DataSource = null;
            lbxCadastro.DataBind();
        }

        private bllRetorno Inserir(ListItem _item)
        {
            nutracDietasDcl = new NutraceuticosDieta();

            nutracDietasDcl.IdNutrac = Funcoes.Funcoes.ConvertePara.Int(
                ddlNutrac.SelectedValue);
            nutracDietasDcl.IdDieta = Funcoes.Funcoes.ConvertePara.Int(_item.Value);
            nutracDietasDcl.Observacao = "";

            nutracDietasDcl.Ativo = true;
            nutracDietasDcl.IdOperador = Funcoes.Funcoes.ConvertePara.Int(User.Identity.Name);
            nutracDietasDcl.DataCadastro = DateTime.Now;
            nutracDietasDcl.IP = Request.UserHostAddress;

            bllRetorno inserirRet = nutracDietasBll.Inserir(nutracDietasDcl);

            return inserirRet;
        }

        private bllRetorno Excluir(ListItem _item)
        {
            bllRetorno excluirRet = nutracDietasBll.Excluir(
                Funcoes.Funcoes.ConvertePara.Int(_item.Value),
                Funcoes.Funcoes.ConvertePara.Int(ddlNutrac.SelectedValue));

            return excluirRet;
        }
    }
}